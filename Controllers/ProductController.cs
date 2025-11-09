using Microsoft.AspNetCore.Authorization;
using LatestEcommAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;


namespace MyWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetAllProducts()
        {
            using (var connection = new SqliteConnection("Data source=Data/db.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM product";

                command.ExecuteNonQuery();

                var reader = command.ExecuteReader();
                var products = new List<object>();
                while (reader.Read())
                {
                    products.Add(new
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2)
                    });
                }

                return Ok(new { message = "200", products });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            using (var connection = new SqliteConnection("Data source=Data/db.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM product WHERE id = $id";

                command.Parameters.AddWithValue("$id", id);

                command.ExecuteNonQuery();

                var reader = command.ExecuteReader();
                var products = new List<object>();

                if (!reader.Read())
                {
                    return NotFound(new { error = "Product not found" });
                }

                products.Add(new
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2)
                });
                return Ok(new { message = "200", product = products });
            }
        }


        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            using (var connection = new SqliteConnection("Data source=Data/db.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO product (name, price) VALUES ($name, $price)";

                command.Parameters.AddWithValue("$name", product.Name);
                command.Parameters.AddWithValue("$price", product.Price);

                command.ExecuteNonQuery();
                return Ok(new { message = "Product created!", product });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            using (var connection = new SqliteConnection("Data source=Data/db.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "DELETE FROM product WHERE id = $id";

                command.Parameters.AddWithValue("$id", id);
            }
            return Ok(new { message = "Product deleted successfully" });
        }


        [HttpPatch("{id}")]
        public IActionResult UpdateStock([FromRoute] int id, Product product)
        {
            using (var connection = new SqliteConnection("Data source=Data/db.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE product set price = $price where id = $id";

                command.Parameters.AddWithValue("$price", product.Price);
                command.Parameters.AddWithValue("$id", id);

                int affectedRows = command.ExecuteNonQuery();

                return (affectedRows >= 1) ? Ok(new { message = "The product has been Updated" }) : NotFound(new { message = "Product not found" });
            }
        }

    }

}
