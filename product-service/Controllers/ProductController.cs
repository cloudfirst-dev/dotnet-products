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
        private readonly IConfiguration Configuration;


        private readonly ILogger<ProductController> _logger;
        private static readonly HttpClient client = new HttpClient();

        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
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

            return new Product {
                Votes = rating.Votes
            };
        }
    }
}
