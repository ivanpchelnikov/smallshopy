using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmalShopy.Models
{
    public class Special
    {
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        public decimal TotalSavings { get; set; }
        [JsonPropertyName("quantities")]
        public IEnumerable<Product> Quantities { get; set; }
        public int Includes { get; set; }
    }
}

