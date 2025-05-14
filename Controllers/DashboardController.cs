using AgriEnergyConnectApp.Data;
using AgriEnergyConnectApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnectApp.Controllers
{
    using Microsoft.Extensions.Logging; // Required for ILogger

    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ApplicationDbContext context, ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult FarmerDashboard()
        {
            try
            {
                var farmer = _context.Users.FirstOrDefault(u => u.Role == UserRole.Farmer);
                if (farmer == null)
                {
                    _logger.LogWarning("No farmer user found.");
                    return RedirectToAction("Login", "Account");
                }

                var products = _context.Products
                    .Where(p => p.FarmerId == farmer.Id)
                    .ToList();

                _logger.LogInformation($"Retrieved {products.Count} products for farmer with ID {farmer.Id}");

                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading farmer dashboard.");
                return View("Error", new ErrorViewModel { Message = "An error occurred while loading your dashboard." });
            }
        }

        public IActionResult EmployeeDashboard()
        {
            return View();
        }
    }

}
