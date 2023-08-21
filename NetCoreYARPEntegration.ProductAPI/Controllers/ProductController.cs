using Microsoft.AspNetCore.Mvc;

namespace NetCoreYARPEntegration.ProductAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok("GetProducts");
        }
    }
}