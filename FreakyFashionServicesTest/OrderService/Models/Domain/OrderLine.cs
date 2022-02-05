using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrderService.Models.Domain
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }     
        public int OrderId { get; set; } 
        public int ProductId { get; set; }
        public int Quantity { get; set; }


    }
}
