using MassTransit;

namespace Inventory.Comsumers
{
    public class InventoryComsumer : IConsumer<Entities.Inventory>
    {
        public async Task Consume(ConsumeContext<Entities.Inventory> context)
        {
            var message = context.Message;
            return;
        }
    }
}
