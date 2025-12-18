using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductStore.Data;

namespace ProductStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
            .Include(o =>o.Items)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

            return View(orders);
        }
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}
