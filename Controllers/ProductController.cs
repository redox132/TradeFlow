using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public IActionResult GetAllProducts()
        {
            return Ok(new { products = "These are the products" });
        }
    }
}
