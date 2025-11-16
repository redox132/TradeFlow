using LatestEcommAPI.Models;
using LatestEcommAPI.DTOs.Order;
using LatestEcommAPI.DTOs.ShipmentDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
namespace LatestEcommAPI.Controllers;

[ApiController]
[Route("api/")]
public class FulfilmentController : ControllerBase
{

    [HttpPost("order/{id}/fulfil")]
    [Authorize]
    public async Task<IActionResult> FulFil([FromBody] ShipmentDetailsDto shipmentDetailsDto, int id)
    {
        using (var connection = new SqliteConnection("Data source=Data/db.db"))
        {
            var shipmentDetails = new ShipmentDetailsDto
            {
                CarrierId = shipmentDetailsDto.CarrierId,
                TrackingNumber = shipmentDetailsDto.TrackingNumber,
                AddressLine1 = shipmentDetailsDto.AddressLine1,
                City = shipmentDetailsDto.City,
                Postcode = shipmentDetailsDto.Postcode,
                Country = shipmentDetailsDto.Country,
            };

            var serialized = JsonSerializer.Serialize(
                shipmentDetails,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            await connection.OpenAsync();
            
         var command = connection.CreateCommand();
            command.CommandText = $"UPDATE orders SET shipment_details = '{serialized}', status = 'shipped' WHERE id = $id ";
            command.Parameters.AddWithValue("$id", id);

            int affectedRows = await command.ExecuteNonQueryAsync();

            if (affectedRows == 0)
            {
                return NotFound(new { message = "Order not found" });
            }

            if (shipmentDetailsDto.CarrierId != 1)
            {
                return NotFound(new { message = $"The id provided is invalid carrier id: {shipmentDetails.CarrierId}" });
            }

            return Ok(new { message = "OK", orderId = id, res = shipmentDetails });
        }

    }
}
