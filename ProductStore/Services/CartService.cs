using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProductStore.Models.Cart;

namespace ProductStore.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartKey = "cart";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Отримати кошик
        public List<CartItem> GetCart()
        {
            var session = _httpContextAccessor.HttpContext!.Session;
            var cartJson = session.GetString(CartKey);

            if (string.IsNullOrEmpty(cartJson))
                return new List<CartItem>();

            return JsonConvert.DeserializeObject<List<CartItem>>(cartJson)!;
        }

        // Зберегти кошик
        private void SaveCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext!.Session;
            var json = JsonConvert.SerializeObject(cart);
            session.SetString(CartKey, json);
        }

        // Додати товар
        public void AddToCart(CartItem item)
        {
            var cart = GetCart();

            var existing = cart.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (existing == null)
            {
                cart.Add(item);
            }
            else
            {
                existing.Quantity += item.Quantity;
            }

            SaveCart(cart);
        }

        // Змінити кількість
        public void UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();

            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                if (item.Quantity <= 0)
                    cart.Remove(item);
            }

            SaveCart(cart);
        }

        // Видалити товар
        public void Remove(int productId)
        {
            var cart = GetCart();
            cart.RemoveAll(x => x.ProductId == productId);
            SaveCart(cart);
        }

        // Очистити кошик
        public void Clear()
        {
            SaveCart(new List<CartItem>());
        }
    }
}