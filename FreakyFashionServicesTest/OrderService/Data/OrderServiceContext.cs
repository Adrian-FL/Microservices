using Microsoft.EntityFrameworkCore;
using OrderService.Models.Domain;

namespace OrderService.Data
{
    public class OrderServiceContext :DbContext 
    {
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public OrderServiceContext(DbContextOptions<OrderServiceContext> options) : 
            base(options)
        {

        }
    }
}
