using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ps.Ecomm.DataAccess;
using Ps.Ecomm.Models;
using System.Text.Json;

namespace Ps.Ecomm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductProvider _productProvider;
        private readonly IPublishEndpoint publisher;

        public ProductController(IProductProvider productProvider, IPublishEndpoint publisher)
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
            await publisher.Publish<Product>(product);
            return Ok();
        }
    }
}
