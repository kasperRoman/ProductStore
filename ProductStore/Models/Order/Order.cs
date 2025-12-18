using System.ComponentModel.DataAnnotations;

namespace ProductStore.Models.Order
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required]
        public string CustomerPhone { get; set; } = string.Empty;

        [Required]
        public string CustomerAddress { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }
}