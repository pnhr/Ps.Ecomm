using MassTransit;
using Ps.Ecomm.DataAccess;
using Ps.Ecomm.Models;

namespace Ps.Ecomm.Listeners
{
    public class OrderCreatedConsumer : IConsumer<OrderRequest>
    {
        private readonly IInventoryUpdator inventoryUpdator;
        private readonly IPublishEndpoint publiser;

        public OrderCreatedConsumer(IInventoryUpdator inventoryUpdator, IPublishEndpoint publiser)
        {
            this.inventoryUpdator = inventoryUpdator;
            this.publiser = publiser;
        }
        public async Task Consume(ConsumeContext<OrderRequest> context)
        {
            try
            {
                await inventoryUpdator.UpdateAsync(context.Message.ProductId, context.Message.Quantity);
                await publiser.Publish(new InventoryResponse
                {
                    IsSuccess= true,
                    OrderId = context.Message.OrderId,
                });
            }
            catch (Exception)
            {
                await publiser.Publish(new InventoryResponse
                {
                    IsSuccess = false,
                    OrderId = context.Message.OrderId,
                });
            }
        }
    }
}
