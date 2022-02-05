using Microsoft.AspNetCore.Mvc;
using FreakyFashionServicesTest.StockService.Data;
using FreakyFashionServicesTest.CatalogService.Models.DTO;
using FreakyFashionServicesTest.CatalogService.Models.Domain;
using CatalogService.Models.DTO;

namespace CatalogService.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public CatalogServiceContext Context { get; }

        public ProductsController(CatalogServiceContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IEnumerable<ProductDto> GetAll()
        {
            var products = Context.Products.Select(x => new ProductDto
            {
                Id = x.Id,
                Price = x.Price,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                UrlSlug = x.UrlSlug,
            });

            return products;
        }

       
        [HttpPost]
        public IActionResult AddProduct(CreateProductDto productDto)
        {

            // used Datetime now millis to make the url slugs unique
            string urlSlug = productDto.Name.ToLower().Replace(' ', '-') + DateTime.Now.Millisecond.ToString();

            var product = new Product
            {
                Price = productDto.Price,
                Name = productDto.Name,
                Description = productDto.Description,
                ImageUrl = productDto.ImageUrl,
                UrlSlug = urlSlug
            };

            Context.Products.Add(product);

            Context.SaveChanges();

            ProductDto createdProduct = new ProductDto
            {
                Id = product.Id,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                UrlSlug = urlSlug
            };

            return Created("", createdProduct); // 201 Created
        }
    }
}
