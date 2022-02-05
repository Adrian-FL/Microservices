using System.Text.Json.Serialization;

namespace BasketService.Models
{
    public class Product 
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity{ get; set; }
    }
}
