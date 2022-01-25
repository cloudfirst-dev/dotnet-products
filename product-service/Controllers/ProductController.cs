using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace product_service.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new List<Product>();
        private static Boolean init = false;
        private readonly IConfiguration Configuration;


        private readonly ILogger<ProductController> _logger;
        private static readonly HttpClient client = new HttpClient();

        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;

            if(!init) {
                products.Add(new Product {
                    Name = "Soccer Ball",
                    Id = products.Count
                });
                init = true;
            }
        }

        [HttpGet]
        [Route("/product")]
        public List<Product> Get() {
            return products;
        }

        [HttpPost]
        [Route("/product")]
        public void Post([FromBody] Product product) {
            Console.WriteLine("Creating new product " + products.Count);
            product.Id = products.Count;
            products.Add(product);
            Console.WriteLine("Added new product " + products.Count);
        }

        [HttpGet]
        [Route("/product/{id}")]
        public async Task<Product> Get(int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var streamTask = client.GetStreamAsync(Configuration["RatingUrl"] + "/rating/" + id);

            var rating = await JsonSerializer.DeserializeAsync<Rating>(await streamTask);

            var product = products[id];
            product.Votes = rating.Votes;

            return product;
        }
    }
}
