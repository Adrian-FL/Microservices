using FreakyFashionServicesTest.CatalogService.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FreakyFashionServicesTest.StockService.Data
{
    public class CatalogServiceContext :DbContext 
    {
        public DbSet<Product> Products { get; set; }

        public CatalogServiceContext(DbContextOptions<CatalogServiceContext> options) 
            : base(options)
        {

        }

    }
}
