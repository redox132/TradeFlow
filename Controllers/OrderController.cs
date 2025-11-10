using LatestEcommAPI.Models;
using LatestEcommAPI.DTOs.Order;
using LatestEcommAPI.DTOs.ShipmentDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Text.Json;

namespace LatestEcommAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpGet("{id}/items")]
        public IActionResult GetOrderItems([FromRoute] int id)
        {
            var orders = new List<OrderDto>();

            using (var connection = new SqliteConnection("Data source=Data/db.db"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {

                    command.CommandText = @"
                        SELECT 
                            o.id AS OrderId,
                            o.order_date AS OrderDate,
                            oi.product_id AS ProductID,
                            oi.quantity AS Quantity,
                            o.shipment_details AS ShipmentDetails
                        FROM orders o
                        JOIN order_item oi ON o.id = oi.order_id
                        WHERE o.user_id = $id;
                    ";
                    command.Parameters.AddWithValue("$id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        int idxOrderId = reader.GetOrdinal("OrderId");
                        int idxOrderDate = reader.GetOrdinal("OrderDate");
                        int idxProductID = reader.GetOrdinal("ProductID");
                        int idxQuantity = reader.GetOrdinal("Quantity");
                        int idxShipmentDetails = reader.GetOrdinal("ShipmentDetails");

                        while (reader.Read())
                        {
                            ShipmentDetailsDto? shipment = null;

                            if (!reader.IsDBNull(idxShipmentDetails))
                            {
                                var raw = reader.GetString(idxShipmentDetails);

                                if (!string.IsNullOrWhiteSpace(raw))
                                {
                                    try
                                    {
                                        shipment = JsonSerializer.Deserialize<ShipmentDetailsDto>(
                                            raw,
                                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                                        );
                                    }
                                    catch (JsonException)
                                    {
                                        // Fallback for plain text: store it in AddressLine1
                                        shipment = new ShipmentDetailsDto { AddressLine1 = raw };
                                    }
                                }
                            }

                            var item = new OrderDto
                            {
                                OrderId = reader.GetInt32(idxOrderId),
                                OrderDate = reader.GetDateTime(idxOrderDate),
                                ProductID = reader.GetInt32(idxProductID),
                                Quantity = reader.GetInt32(idxQuantity),
                                ShipmentDetails = shipment
                            };

                            orders.Add(item);
                        }

                    }
                }
            }

            return Ok(new { message = "OK", orders });
        }
    }
}
