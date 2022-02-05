
using System.Text.Json.Serialization;

namespace FreakyFashionServicesTest.CatalogService.Models.DTO
{
    public class ProductDto
    {
        public ProductDto()
        {
        }

        public ProductDto(float price, string name, string description, string imageUrl)
        {
            Price = price;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }

        public ProductDto(int id, float price, string name, string description, string imageUrl, string urlSlug)
        {
            Id = id;
            Price = price;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            UrlSlug = urlSlug;
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("price")]
        public float Price { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("urlSlug")]
        public string UrlSlug { get; set; }
    }
}
