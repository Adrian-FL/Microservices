
using System.Text.Json.Serialization;

namespace BasketService.Models.DTO
{
    
    public class UpdateBasketDto
    {
        public UpdateBasketDto(string identifier, Product[] items)
        {
            Identifier = identifier;
            Items = items;
        }

        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
        [JsonPropertyName("items")]
        public Product[] Items { get; set; }

    }
}
