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
    public IActionResult FulFil([FromBody] ShipmentDetailsDto shipmentDetailsDto, int id)
    {
        using (var connection = new SqliteConnection("Data source=Data/db.db"))
        {
            var shipmentDetails = new ShipmentDetailsDto
            {
                Carrier = shipmentDetailsDto.Carrier,
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

            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = $"UPDATE orders SET shipment_details = '{serialized}', status = 'shipped' WHERE id = $id ";

            command.Parameters.AddWithValue("$id", id);

            command.ExecuteNonQuery();

            return Ok(new { message = "OK", orderId = id, res = shipmentDetails });
        }

    }
}