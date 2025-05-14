using AgriEnergyConnectApp.Data;
using AgriEnergyConnectApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http; // Required for session

namespace AgriEnergyConnectApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ApplicationDbContext context, ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Original functionality: Farmer Dashboard
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

        // Original functionality: Employee Dashboard
        public IActionResult EmployeeDashboard()
        {
            return View();
        }

        // New action for creating a farmer profile
        public IActionResult CreateFarmerProfile()
        {
            return View(); // View to create the farmer profile
        }

        // POST action for creating the farmer profile
        [HttpPost]
        public IActionResult CreateFarmerProfile(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = model.Password, // ⚠️ Note: Hash passwords in production
                    Role = UserRole.Farmer
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Farmer profile successfully created.";
                return RedirectToAction("EmployeeDashboard");
            }

            // If model state is invalid, return to the view with validation errors
            return View(model);
        }

    }
}
