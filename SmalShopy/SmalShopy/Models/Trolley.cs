using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmalShopy.Models
{
    public class Trolley
    {
        [JsonPropertyName("products")]
        public IEnumerable<Product> Products { get; set; }
        [JsonPropertyName("specials")]
        public IEnumerable<Special> Specials { get; set; }
        [JsonPropertyName("quantities")]
        public IEnumerable<Product> Quantities { get; set; }
    }
}

