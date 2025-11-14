using Microsoft.AspNetCore.Authorization;
using LatestEcommAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;


[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{

    [HttpGet("{id}/items")]
    [Authorize]
    public async Task<IActionResult> GetCartItems([FromRoute] int id)
    {
        using (var connection = new SqliteConnection("Data source=Data/db.db"))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = @"
                Select c.id, c.user_id, ci.product_id, ci.quantity
                From cart c
                Join cart_item ci On c.id = ci.cart_id
                Where c.user_id = $id;
                ";
            command.Parameters.AddWithValue("$id", id);

            var cartItems = new List<object>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (!reader.HasRows)
                {
                    return Ok(new { message = $"No cart items found for user with ID: {id}" });
                }

                while (await reader.ReadAsync())
                {
                    cartItems.Add(new
                    {
                        cartId = reader.GetInt32(0),
                        productId = reader.GetInt32(2),
                        userId = reader.GetInt32(1),
                        quantity = reader.GetInt32(3)
                    });
                }
            }
            return Ok(new { cartItems });
        }
    }
}