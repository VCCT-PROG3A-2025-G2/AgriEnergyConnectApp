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
                // Get the logged-in user's ID from the session
                var userId = HttpContext.Session.GetInt32("UserId");

                if (userId == null)
                {
                    _logger.LogWarning("No user ID in session.");
                    return RedirectToAction("Login", "Account"); // Redirect to login if no user ID found
                }

                // Retrieve the farmer using the user ID and ensure they are a farmer
                var farmer = _context.Users.FirstOrDefault(u => u.Id == userId && u.Role == UserRole.Farmer);

                if (farmer == null)
                {
                    _logger.LogWarning("Logged-in user is not a farmer.");
                    return RedirectToAction("Login", "Account"); // Redirect to login if the user is not a farmer
                }

                // Retrieve the products associated with the logged-in farmer
                var products = _context.Products
                    .Where(p => p.FarmerId == farmer.Id)
                    .ToList();

                _logger.LogInformation($"Retrieved {products.Count} products for farmer with ID {farmer.Id}");

                // Return the view with the products for the logged-in farmer
                return View(products);
            }
            catch (Exception ex)
            {
                // Log any errors that occur
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

        public IActionResult ViewAllProducts(string category, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Products.Include(p => p.User).AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }

            if (startDate.HasValue)
            {
                query = query.Where(p => p.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(p => p.Date <= endDate.Value);
            }

            var viewModel = new ProductFilterViewModel
            {
                Products = query.ToList(),
                Category = category,
                StartDate = startDate,
                EndDate = endDate,
                AvailableCategories = _context.Products
                    .Select(p => p.Category)
                    .Distinct()
                    .ToList()
            };

            return View(viewModel); // This matches the @model in your .cshtml
        }


        public async Task<IActionResult> ViewFarmers()
        {
            var farmers = await _context.Users
                .Where(u => u.Role == UserRole.Farmer)
                .Select(u => new FarmerViewModel
                {
                    Id = u.Id.ToString(),
                    FullName = u.FullName,
                    Email = u.Email
                }).ToListAsync();

            return View(farmers);
        }

        public async Task<IActionResult> ViewFarmerProducts(string farmerId, string category, DateTime? startDate, DateTime? endDate)
        {
            if (!int.TryParse(farmerId, out int farmerIdInt))
            {
                return BadRequest("Invalid Farmer ID");
            }

            var query = _context.Products
                .Include(p => p.User)
                .Where(p => p.FarmerId == farmerIdInt);

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category == category);

            if (startDate.HasValue)
                query = query.Where(p => p.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(p => p.Date <= endDate.Value);

            var categories = await _context.Products
                .Where(p => p.FarmerId == farmerIdInt)
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            var viewModel = new ProductFilterViewModel
            {
                Products = await query.ToListAsync(),
                AvailableCategories = categories,
                Category = category,
                StartDate = startDate,
                EndDate = endDate,
                FarmerId = farmerId // still passing string here
            };

            return View(viewModel);
        }


    }
}
