using Microsoft.AspNetCore.Mvc;
using ProductStore.Data;
using ProductStore.Models.Order;
using ProductStore.Services;

namespace ProductStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly CartService _cartService;
        private readonly AppDbContext _context;

        public CheckoutController(CartService cartService, AppDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        // Показати форму
        public IActionResult Index()
        {
            return View();
        }

        // Прийняти форму
        [HttpPost]
        public IActionResult Index(Order model)
        {
            var cart = _cartService.GetCart();

            if (!cart.Any())
            {
                ModelState.AddModelError("", "Кошик порожній.");
                return View(model);
            }

            // Підрахунок суми
            model.TotalAmount = cart.Sum(x => x.Price * x.Quantity);

            // Додаємо замовлення в базу
            _context.Orders.Add(model);
            _context.SaveChanges();

            // Додаємо товари замовлення
            foreach (var item in cart)
            {
                var orderItem = new OrderItem
                {
                    OrderId = model.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity
                };

                _context.OrderItems.Add(orderItem);

                // Зменшуємо кількість товару на складі
                var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    product.Stock -= item.Quantity;
                }
            }

            _context.SaveChanges();

            // Очищаємо кошик
            _cartService.Clear();

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
