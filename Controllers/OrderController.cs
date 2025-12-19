using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using LatestEcommAPI.DTOs.Order;
using LatestEcommAPI.DTOs.ShipmentDetails;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace LatestEcommAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        // ---------------------------------------------------------
        // GET SPECIFIC ORDER + ITEMS
        // ---------------------------------------------------------
        [HttpGet("{orderId}/items")]
        [Authorize]
        public IActionResult GetOrderItems([FromRoute] int orderId)
        {
            using var connection = new SqliteConnection("Data Source=Data/db.db");
            connection.Open();

            var items = new List<object>();

            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                SELECT 
                    o.id AS OrderId,
                    o.order_date AS OrderDate,
                    o.currency,
                    o.payment_status,
                    o.status,
                    oi.id AS ItemId,
                    oi.product_id,
                    oi.variant_id,
                    oi.quantity,
                    oi.price,
                    oi.catalog_number,
                    oi.ean,
                    oi.name
                FROM orders o
                LEFT JOIN order_items oi ON o.id = oi.order_id
                WHERE o.id = $orderId;
            ";
            cmd.Parameters.AddWithValue("$orderId", orderId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                items.Add(new
                {
                    OrderId = reader.GetInt32(0),
                    OrderDate = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Currency = reader.GetString(2),
                    PaymentStatus = reader.GetString(3),
                    Status = reader.GetInt32(4),

                    ItemId = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                    ProductId = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                    VariantId = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                    Quantity = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                    Price = reader.IsDBNull(9) ? (decimal?)null : reader.GetDecimal(9),
                    CatalogNumber = reader.IsDBNull(10) ? null : reader.GetString(10),
                    EAN = reader.IsDBNull(11) ? null : reader.GetString(11),
                    Name = reader.IsDBNull(12) ? null : reader.GetString(12)
                });
            }

            return Ok(new { message = "OK", orderId, items });
        }


        // ---------------------------------------------------------
        // GET ALL ORDERS FOR USER (via API key)
        // ---------------------------------------------------------
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrders(
            [FromHeader] string X_API_KEY,
            [FromQuery] int size = 15,
            [FromQuery] int page = 1)
        {
            using var connection = new SqliteConnection("Data Source=Data/db.db");
            await connection.OpenAsync();

            // get user
            var userCmd = connection.CreateCommand();
            userCmd.CommandText = "SELECT id FROM users WHERE x_api_key = $api;";
            userCmd.Parameters.AddWithValue("$api", X_API_KEY);

            var uid = await userCmd.ExecuteScalarAsync();
            if (uid == null)
                return Unauthorized(new { message = "Invalid API Key" });

            int userId = Convert.ToInt32(uid);

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                SELECT id, currency, payment_status, paid, status, email, date, order_date
                FROM orders
                WHERE user_id = $userId
                LIMIT $size OFFSET $offset;
            ";
            cmd.Parameters.AddWithValue("$userId", userId);
            cmd.Parameters.AddWithValue("$size", size);
            cmd.Parameters.AddWithValue("$offset", (page - 1) * size);

            var orders = new List<object>();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                orders.Add(new
                {
                    OrderId = reader.GetInt32(0),
                    Currency = reader.GetString(1),
                    PaymentStatus = reader.GetString(2),
                    Paid = reader.IsDBNull(3) ? (decimal?)0 : reader.GetDecimal(3),
                    Status = reader.GetInt32(4),
                    Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Date = reader.IsDBNull(6) ? null : reader.GetString(6),
                    OrderDate = reader.IsDBNull(7) ? null : reader.GetString(7)
                });
            }

            return Ok(new { message = "OK", orders });
        }
    }
}
