using System.Text.Json.Serialization;

namespace OrderService.Models.DTO
{
    public class CreateOrderDto
    {

        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyName("customer")]
        public string Customer { get; set; }

    }
}
