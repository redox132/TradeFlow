using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using LatestEcommAPI.Models;
using Tradeflow.DTOs.Product;
using Tradeflow.Helpers;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authorization;

namespace Tradeflowi.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromHeader(Name = "X-API-KEY")] string X_API_KEY, [FromQuery] int size = 15, [FromQuery] int page = 1)
        {
            var products = new List<object>();

            using (var connection = new SqliteConnection("Data Source=Data/db.db"))
            {
                await connection.OpenAsync();

                // Get products for the user
                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT p.id, p.name, p.catalog_number, p.ean, p.symbol, p.location, p.stock, p.price
                    FROM products p
                    INNER JOIN users u ON p.user_id = u.id
                    WHERE u.x_api_key = $X_API_KEY
                    LIMIT $size OFFSET $offset";
                command.Parameters.AddWithValue("$X_API_KEY", X_API_KEY);
                command.Parameters.AddWithValue("$size", size);
                command.Parameters.AddWithValue("$offset", size * (page - 1));

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int productId = reader.GetInt32(0);

                        // Get variants for the product
                        var variants = new List<object>();
                        var variantCmd = connection.CreateCommand();
                        variantCmd.CommandText = "SELECT id, catalog_number, ean, symbol FROM product_variants WHERE product_id = $productId";
                        variantCmd.Parameters.AddWithValue("$productId", productId);

                        using (var variantReader = await variantCmd.ExecuteReaderAsync())
                        {
                            while (await variantReader.ReadAsync())
                            {
                                variants.Add(new
                                {
                                    Id = variantReader.GetInt32(0),
                                    CatalogNumber = variantReader.GetString(1),
                                    EAN = variantReader.GetString(2),
                                    Symbol = variantReader.GetString(3)
                                });
                            }
                        }

                        products.Add(new
                        {
                            Id = productId,
                            Name = reader.GetString(1),
                            CatalogNumber = reader.IsDBNull(2) ? null : reader.GetString(2),
                            EAN = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Symbol = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Location = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Stock = reader.GetInt32(6),
                            Price = reader.GetDecimal(7),
                            Variants = variants
                        });
                    }
                }
            }

            return Ok(new { message = "200", products });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromHeader(Name = "X-API-KEY")] string X_API_KEY, [FromBody] ProductCreateDto productDto)
        {
            // ...
            using (var connection = new SqliteConnection("Data Source=Data/db.db"))
            {
                if (!ModelState.IsValid)
                {
                    // Model binding failed or validation rules were violated
                    return BadRequest(ModelState);
                }
                await connection.OpenAsync();

                // Get user ID
                var getUserCmd = connection.CreateCommand();
                getUserCmd.CommandText = "SELECT id FROM users WHERE x_api_key = $X_API_KEY LIMIT 1";
                getUserCmd.Parameters.AddWithValue("$X_API_KEY", X_API_KEY);

                var userIdObj = await getUserCmd.ExecuteScalarAsync();
                if (userIdObj == null) return Unauthorized(new { message = "Invalid API Key" });

                int userId = Convert.ToInt32(userIdObj);

                // Insert product
                var insertProductCmd = connection.CreateCommand();
                insertProductCmd.CommandText = @"
                    INSERT INTO products (user_id, name, catalog_number, ean, symbol, location, stock, price)
                    VALUES ($user_id, $name, $catalog_number, $ean, $symbol, $location, $stock, $price);
                    SELECT last_insert_rowid();";
                insertProductCmd.Parameters.AddWithValue("$user_id", userId);
                insertProductCmd.Parameters.AddWithValue("$name", productDto.Name);
                insertProductCmd.Parameters.AddWithValue("$catalog_number", productDto.CatalogNumber ?? (object)DBNull.Value);
                insertProductCmd.Parameters.AddWithValue("$ean", productDto.EAN ?? (object)DBNull.Value);
                insertProductCmd.Parameters.AddWithValue("$symbol", productDto.Symbol ?? (object)DBNull.Value);
                insertProductCmd.Parameters.AddWithValue("$location", productDto.Location ?? (object)DBNull.Value);
                insertProductCmd.Parameters.AddWithValue("$stock", productDto.Stock);
                insertProductCmd.Parameters.AddWithValue("$price", productDto.Price);

                var productId = (long?)await insertProductCmd.ExecuteScalarAsync();


                // Insert variants
                if (productDto.Variants != null)
                {
                    foreach (var variant in productDto.Variants)
                    {
                        var insertVariantCmd = connection.CreateCommand();
                        insertVariantCmd.CommandText = @"
                            INSERT INTO product_variants (product_id, catalog_number, ean, symbol)
                            VALUES ($product_id, $catalog_number, $ean, $symbol)";
                        insertVariantCmd.Parameters.AddWithValue("$product_id", productId);
                        insertVariantCmd.Parameters.AddWithValue("$catalog_number", variant.CatalogNumber ?? (object)DBNull.Value);
                        insertVariantCmd.Parameters.AddWithValue("$ean", variant.EAN ?? (object)DBNull.Value);
                        insertVariantCmd.Parameters.AddWithValue("$symbol", variant.Symbol ?? (object)DBNull.Value);

                        await insertVariantCmd.ExecuteNonQueryAsync();
                    }
                }

                return Ok(new { message = "Product created", productId });
            }
        }
        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateProduct([FromHeader(Name = "X-API-KEY")] string X_API_KEY, [FromRoute] int productId, [FromBody] ProductUpdateDto dto)
        {
            using (var connection = new SqliteConnection("Data Source=Data/db.db"))
            {
                await connection.OpenAsync();

                // Verify user
                var getUserCmd = connection.CreateCommand();
                getUserCmd.CommandText = "SELECT id FROM users WHERE x_api_key = $X_API_KEY LIMIT 1";
                getUserCmd.Parameters.AddWithValue("$X_API_KEY", X_API_KEY);

                var userObj = await getUserCmd.ExecuteScalarAsync();
                if (userObj == null)
                    return Unauthorized(new { message = "Invalid API Key" });

                int userId = Convert.ToInt32(userObj);

                // Check product exists and belongs to user
                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = "SELECT COUNT(*) FROM products WHERE id = $id AND user_id = $user_id";
                checkCmd.Parameters.AddWithValue("$id", productId);
                checkCmd.Parameters.AddWithValue("$user_id", userId);

                long exists = (long)await checkCmd.ExecuteScalarAsync();
                if (exists == 0)
                    return NotFound(new { message = "Product not found" });

                // Build dynamic SQL
                var updates = new List<string>();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE products SET ";

                if (dto.Name != null)
                {
                    updates.Add("name = $name");
                    cmd.Parameters.AddWithValue("$name", dto.Name);
                }
                if (dto.CatalogNumber != null)
                {
                    updates.Add("catalog_number = $catalog_number");
                    cmd.Parameters.AddWithValue("$catalog_number", dto.CatalogNumber);
                }
                if (dto.EAN != null)
                {
                    updates.Add("ean = $ean");
                    cmd.Parameters.AddWithValue("$ean", dto.EAN);
                }
                if (dto.Symbol != null)
                {
                    updates.Add("symbol = $symbol");
                    cmd.Parameters.AddWithValue("$symbol", dto.Symbol);
                }
                if (dto.Location != null)
                {
                    updates.Add("location = $location");
                    cmd.Parameters.AddWithValue("$location", dto.Location);
                }
                if (dto.Stock.HasValue)
                {
                    updates.Add("stock = $stock");
                    cmd.Parameters.AddWithValue("$stock", dto.Stock);
                }
                if (dto.Price.HasValue)
                {
                    updates.Add("price = $price");
                    cmd.Parameters.AddWithValue("$price", dto.Price);
                }

                if (updates.Count > 0)
                {
                    cmd.CommandText += string.Join(", ", updates) + " WHERE id = $productId";
                    cmd.Parameters.AddWithValue("$productId", productId);
                    await cmd.ExecuteNonQueryAsync();
                }

                // Handle variants
                if (dto.Variants != null)
                {
                    foreach (var v in dto.Variants)
                    {
                        var variantCmd = connection.CreateCommand();
                        variantCmd.CommandText = @"
                    UPDATE product_variants
                    SET catalog_number = $catalog, ean = $ean, symbol = $symbol
                    WHERE id = $id AND product_id = $productId";
                        variantCmd.Parameters.AddWithValue("$catalog", (object?)v.CatalogNumber ?? DBNull.Value);
                        variantCmd.Parameters.AddWithValue("$ean", (object?)v.EAN ?? DBNull.Value);
                        variantCmd.Parameters.AddWithValue("$symbol", (object?)v.Symbol ?? DBNull.Value);
                        variantCmd.Parameters.AddWithValue("$id", v.Id);
                        variantCmd.Parameters.AddWithValue("$productId", productId);

                        await variantCmd.ExecuteNonQueryAsync();
                    }
                }

                return Ok(new { message = "Product updated" });
            }
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct([FromHeader(Name = "X-API-KEY")] string apiKey, [FromRoute] int productId)
        {
            using (var connection = new SqliteConnection("Data Source=Data/db.db"))
            {
                await connection.OpenAsync();

                // Get the user ID from the API key
                int? userId = await UserHelper.GetRequestCallerUserId(apiKey);

                // Get product
                var productCmd = connection.CreateCommand();
                productCmd.CommandText = @"
                    SELECT id, name, catalog_number, ean, symbol, location, stock, price, is_active
                    FROM products
                    WHERE id = $productId AND user_id = $userId";
                productCmd.Parameters.AddWithValue("$productId", productId);
                productCmd.Parameters.AddWithValue("$userId", userId);

                using var reader = await productCmd.ExecuteReaderAsync();
                if (!await reader.ReadAsync())
                    return NotFound(new { message = "Product not found" });

                var product = new
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    CatalogNumber = reader.IsDBNull(2) ? null : reader.GetString(2),
                    EAN = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Symbol = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Location = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Stock = reader.GetInt32(6),
                    Price = reader.GetDecimal(7),
                    IsActive = reader.GetInt32(8) == 1
                };

                // Get variants for the product
                var variants = new List<object>();
                var variantCmd = connection.CreateCommand();
                variantCmd.CommandText = @"
                    SELECT id, catalog_number, ean, symbol
                    FROM product_variants
                    WHERE product_id = $productId";
                variantCmd.Parameters.AddWithValue("$productId", productId);

                using var variantReader = await variantCmd.ExecuteReaderAsync();
                while (await variantReader.ReadAsync())
                {
                    variants.Add(new
                    {
                        Id = variantReader.GetInt32(0),
                        CatalogNumber = variantReader.IsDBNull(1) ? null : variantReader.GetString(1),
                        EAN = variantReader.IsDBNull(2) ? null : variantReader.GetString(2),
                        Symbol = variantReader.IsDBNull(3) ? null : variantReader.GetString(3)
                    });
                }

                return Ok(new { message = "200", product = product, variants });
            }
        }

    }
}
