
using System.ComponentModel.DataAnnotations;

namespace FreakyFashionServicesTest.CatalogService.Models.Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public float Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string UrlSlug { get; set; }

    }
}
