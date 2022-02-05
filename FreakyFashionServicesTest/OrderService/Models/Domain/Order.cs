using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderService.Models.Domain
{
    public class Order
    {
        [Key]
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyName("customer")]
        public string Customer { get; set; }
    }
}
