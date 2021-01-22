using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmalShopy.Models
{
    public class Customer
    {
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("products")]
        public IEnumerable<Product> ProductList { get; set; }
    }
}
