using Microsoft.AspNetCore.Mvc;
using ProductStore.Services;

namespace ProductStore.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult Increase(int id)
        {
            var cart = _cartService.GetCart();
            var item = cart.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                item.Quantity++;
                _cartService.UpdateQuantity(id, item.Quantity);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Decrease(int id)
        {
            var cart = _cartService.GetCart();
            var item = cart.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                item.Quantity--;
                _cartService.UpdateQuantity(id, item.Quantity);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            _cartService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
