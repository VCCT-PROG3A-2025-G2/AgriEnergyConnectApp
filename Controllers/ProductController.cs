using AgriEnergyConnectApp.Data;
using AgriEnergyConnectApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgriEnergyConnectApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Add Product

        public IActionResult Add()
        {
            return View(new Product()); // ✅ This goes to Views/Product/Add.cshtml
        }


        [HttpPost]
        public IActionResult Add(Product model)
        {
            // ✅ Check if user is logged in
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["Error"] = "You must be logged in to add a product.";
                return RedirectToAction("Login", "Account");
            }

            // ✅ Check if the logged-in user is a farmer
            var farmer = _context.Users.FirstOrDefault(u => u.Id == userId && u.Role == UserRole.Farmer);
            if (farmer == null)
            {
                TempData["Error"] = "No farmer found with this account.";
                return RedirectToAction("Login", "Account");
            }

            // ✅ Assign the FarmerId before validation
            model.FarmerId = farmer.Id;

            // ✅ Validate the complete model (now includes FarmerId)
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                TempData["Error"] = "Validation Errors: " + string.Join("; ", errors);
                return View(model);
            }


            // ✅ Save product to database
            _context.Products.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "Product saved successfully!";
            return RedirectToAction("FarmerDashboard", "Dashboard");
        }


    }
}
