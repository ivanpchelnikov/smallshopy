using System.Text.Json.Serialization;

namespace SmalShopy.Models
{
    public class Product
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

    }
}
