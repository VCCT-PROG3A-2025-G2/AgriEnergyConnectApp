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
                // Retrieve the employee's user ID from the session
                var employeeId = HttpContext.Session.GetInt32("UserId");

                if (employeeId == null)
                {
                    return RedirectToAction("Login", "Account"); // Redirect if employee is not logged in
                }

                // Create a new farmer user profile
                var farmer = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = model.Password, // Ensure to hash the password in production
                    Role = UserRole.Farmer
                };

                // Add the farmer to the Users table
                _context.Users.Add(farmer);
                _context.SaveChanges();

                // Log the creation of the farmer profile
                _logger.LogInformation($"Farmer profile created with ID: {farmer.Id}");

                // Redirect back to employee dashboard after creation
                return RedirectToAction("EmployeeDashboard", "Dashboard");
            }

            // Return the view with validation errors if model is invalid
            return View(model);
        }
    }
}
