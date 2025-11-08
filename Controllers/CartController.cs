using Microsoft.AspNetCore.Mvc;
using LatestEcommAPI.Models;
using Microsoft.Data.Sqlite;

namespace LatestEcommAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{

    [HttpGet("{id}/items")]
    public IActionResult GetCartItems(int id)
    {
        using (var connection = new SqliteConnection("Data source=Data/db.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
            SELECT ci.id, ci.product_id, ci.quantity, c.id AS cart_id, c.user_id
            FROM cart c
            JOIN cart_item ci ON c.id = ci.cart_id
            WHERE c.user_id = $id;
        ";
            command.Parameters.AddWithValue("$id", id);

            var reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                return Ok(new { message = $"No products found for user with ID {id}" });
            }

            var items = new List<object>();
            while (reader.Read())
            {
                items.Add(new
                {
                    cartItemId = reader.GetInt32(0),
                    productId = reader.GetInt32(1),
                    quantity = reader.GetInt32(2),
                    cartId = reader.GetInt32(3),
                    userId = reader.GetInt32(4)
                });
            }

            return Ok(new { message = "OK", items });
        }
    }

}