namespace CatalogService.Models.DTO
{
    public class CreateProductDto
    {
        public float Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
