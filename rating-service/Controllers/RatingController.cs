using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace rating_service.Controllers
{
    [ApiController]
    public class RatingController : ControllerBase
    {
        Dictionary<int, int> votes = new Dictionary<int, int>() {
            {1, 1},
        };

        private readonly ILogger<RatingController> _logger;

        public RatingController(ILogger<RatingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("rating/{id}")]
        public Rating Get(int id)
        {
            if(votes.ContainsKey(id)) {
                return new Rating {
                    Votes = votes[id]
                };
            } else {
                return new Rating();
            }
            
        }
    }
}
