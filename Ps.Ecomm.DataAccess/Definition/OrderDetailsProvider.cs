using Microsoft.Extensions.Logging;
using Ps.Ecomm.Models;
using System.Text.Json;

namespace Ps.Ecomm.DataAccess.Definition
{
    public class OrderDetailsProvider : IOrderDetailsProvider
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrderDetailsProvider> logger;

        public OrderDetailsProvider(IHttpClientFactory httpClientFactory,
            ILogger<OrderDetailsProvider> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<OrderDetail[]> Get()
        {
            try
            {
                using var client = httpClientFactory.CreateClient("order");
                var response = await client.GetAsync("/api/order");
                var data = await response.Content.ReadAsStringAsync();
                OrderDetail[] obj = JsonSerializer.Deserialize<OrderDetail[]>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return obj.ToArray();
            }
            catch (System.Exception exc)
            {
                logger.LogError($"Error getting order details {exc}");
                return Array.Empty<OrderDetail>();
            }
        }
    }
}
