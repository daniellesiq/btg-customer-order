using Core.Entities;
using MassTransit;

namespace Worker.Messaging
{
    public class OrderConsumer : IConsumer<OrderEvent>
    {
        private readonly ILogger<OrderConsumer> _logger;

        public OrderConsumer(ILogger<OrderConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderEvent> context)
        {
            var orderId = context.Message?.OrderId;
            try
            {
                _logger.LogInformation($"Recebendo evento: {nameof(OrderEvent)}: {orderId} ");

                //await context.Publish();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
