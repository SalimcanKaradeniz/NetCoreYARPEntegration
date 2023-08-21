using Microsoft.AspNetCore.Mvc;

namespace NetCoreYARPEntegration.ShippingAPI.Controllers
{
    [ApiController]
    [Route("api/shippings")]
    public class ShippingController : ControllerBase
    {
        private readonly ILogger<ShippingController> _logger;

        public ShippingController(ILogger<ShippingController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetShippings")]
        public async Task<IActionResult> GetShippings()
        {
            return Ok("GetShippings");
        }
    }
}