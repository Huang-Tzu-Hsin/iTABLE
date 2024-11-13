using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurant.Models;
using System.Diagnostics;

namespace restaurant.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ITableDbContext _context;
        public RestaurantController(ITableDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var restaurant = await _context.Restaurants
               .Include(r => r.Announcements)           //串接公告表
               .Include(r => r.Reviews)                 //串接評論
               .Include(r => r.Favorites)               //串接收藏
               .FirstOrDefaultAsync(m => m.RestaurantId == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }
        public async Task<IActionResult> List()
        {
            return View(await _context.Restaurants.ToListAsync());
        }
    }
}
