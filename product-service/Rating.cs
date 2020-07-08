using System;
using System.Text.Json.Serialization;

namespace product_service
{
    public class Rating
    {

        [JsonPropertyName("votes")]
        public int Votes { get; set; }
    }
}
