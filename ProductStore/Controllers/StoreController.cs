using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductStore.Data;
using ProductStore.Models.Cart;
using ProductStore.Services;

namespace ProductStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly AppDbContext _context;

        private readonly CartService _cartService;

        public StoreController(AppDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        // Показ усіх товарів для користувача (фронтенд)
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // Показ одного товару (деталі)
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            var item = new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1
            };

            _cartService.AddToCart(item);

            return RedirectToAction("Index", "Store");
        }
    }
}
