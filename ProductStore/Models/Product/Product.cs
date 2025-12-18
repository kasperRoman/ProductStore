using System.ComponentModel.DataAnnotations;

namespace ProductStore.Models.Product
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва товару є обов'язковою")]
        [StringLength(100, ErrorMessage = "Назва не може бути довшою за 100 символів")]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 1000000, ErrorMessage = "Ціна повинна бути від 0.01 до 1 000 000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Категорія є обов'язковою")]
        [StringLength(50, ErrorMessage = "Категорія не може бути довшою за 50 символів")]
        public string Category { get; set; } = string.Empty;

        [Range(0, 100000, ErrorMessage = "Кількість повинна бути від 0 до 100 000")]
        public int Stock { get; set; }
    }
}