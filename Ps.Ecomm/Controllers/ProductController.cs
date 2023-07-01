using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ps.Ecomm.DataAccess;
using Ps.Ecomm.Models;
using Ps.Ecomm.PlaneRabbitMQ;
using System.Text.Json;

namespace Ps.Ecomm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductProvider _productProvider;
        private readonly IPublisher publisher;

        public ProductController(IProductProvider productProvider, IPublisher publisher)
        {
            _productProvider = productProvider;
            this.publisher = publisher;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Product[] products = await _productProvider.Get();
            return Ok(products);
        }
        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            IDictionary<string, object> messageMetadata = new Dictionary<string, object>();
            messageMetadata.Add(MQConstants.OBJECT_TYPE, Convert.ToString(product));
            publisher.Publish(JsonSerializer.Serialize(product), MQConstants.ROUTE_KEY_REPORT_PRODUCT, messageMetadata);
            return await Task.FromResult(Ok());
        }
    }
}
