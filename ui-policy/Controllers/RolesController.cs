using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ui_policy.Service;
using ui_policy.Dto;

namespace ui_policy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IOpaService _opaService;

        public RolesController(ILogger<RolesController> logger, IOpaService opaService)
        {
            _logger = logger;
            _opaService = opaService;
        }

        [HttpGet]
        public async Task<IEnumerable<String>> Get()
        {
            this.Request.Headers.TryGetValue("Authorization", out var authorization);
            var JwtToken = authorization.First().Split(" ")[1];
            
            
            OpaQueryRequest queryRequest = new OpaQueryRequest {
                Input = new Input {
                    Request = new IncomingRequest {
                        JwtToken = JwtToken
                    }
                }
            };
            var opaResponse = await _opaService.QueryOpaAsync(queryRequest);
            return opaResponse.Result.Roles;
        }
    }
}
