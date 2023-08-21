using Microsoft.AspNetCore.Mvc;

namespace NetCoreYARPEntegration.OrderAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok("GetOrders");
        }
    }
}