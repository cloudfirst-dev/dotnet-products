using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ui_policy.Dto {
    public class Input {
        public IncomingRequest Request { get; set; } = new IncomingRequest();
        [JsonExtensionData]
        public Dictionary<string, JToken> Enriched { get; set; } = new Dictionary<string, JToken>();
    }
}