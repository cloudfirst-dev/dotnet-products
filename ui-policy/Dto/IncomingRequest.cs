using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ui_policy.Dto {
    public class IncomingRequest {
        public string JwtToken { get; set; } = string.Empty;
    }
}