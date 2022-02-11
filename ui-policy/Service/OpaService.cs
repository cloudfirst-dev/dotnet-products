using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ui_policy.Configuration;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Options;
using ui_policy.Dto;
using System.Text;

namespace ui_policy.Service
{
    public sealed class OpaService : IOpaService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerSettings _serializerOptions;
        private readonly string _policyPath;

        public OpaService(OpaConfiguration configuration)
        {
            Console.WriteLine(configuration.BaseAddress);
            _client = new HttpClient
            {
                BaseAddress = new Uri(configuration.BaseAddress),
                Timeout = TimeSpan.FromSeconds(configuration.Timeout),
            };

            _serializerOptions = new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ",
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy(),
                },
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Converters =
                {
                    new StringEnumConverter(new CamelCaseNamingStrategy()),
                },
            };

            _policyPath = configuration.PolicyPath;
        }

        public async Task<OpaQueryResponse> QueryOpaAsync(OpaQueryRequest queryRequest)
        {
            var body = JsonConvert.SerializeObject(queryRequest, _serializerOptions);
            var bodyHttpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PostAsync(_policyPath, bodyHttpContent);

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new OpaAuthorizationMiddlewareException(
                    $"OPA returned bad response code: {httpResponse.StatusCode}");
            }

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            if (stringResponse == null)
            {
                throw new OpaAuthorizationMiddlewareException("OPA returned empty response body");
            }

            try
            {
                var response = JsonConvert.DeserializeObject<OpaQueryResponse>(stringResponse, _serializerOptions);

                if (response == null)
                {
                    throw new OpaAuthorizationMiddlewareException("OPA returned badly formatted response body");
                }

                return response;
            }
            catch (Exception e)
            {
                throw new OpaAuthorizationMiddlewareException("OPA returned badly formatted response body", e);
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
