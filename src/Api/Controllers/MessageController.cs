using Core.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace btg_customer_order.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IPublishEndpoint _publisher;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IPublishEndpoint publisher, ILogger<MessageController> logger)
        {
            _publisher = publisher;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageAsync([FromBody] OrderEvent orderEvent, CancellationToken cancellationToken)
        {
            if (orderEvent != null)
            {
                await _publisher.Publish<OrderEvent>(orderEvent, cancellationToken);

                _logger.LogInformation($"Sent event: {nameof(OrderEvent.OrderId)}");

                return Ok();
            }
            return BadRequest();
        }
    }
}
