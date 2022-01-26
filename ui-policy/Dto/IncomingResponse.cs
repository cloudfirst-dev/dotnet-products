using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ui_policy.Dto {
    public class IncomingResponse {
        public bool Allow { get; set; } = false;
        public List<string> Roles { get; set; } = new List<string> {};
    }
}