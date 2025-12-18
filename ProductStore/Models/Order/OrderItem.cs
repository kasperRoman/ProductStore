using System.ComponentModel.DataAnnotations;

namespace ProductStore.Models.Order
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Зв’язок з Order
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
