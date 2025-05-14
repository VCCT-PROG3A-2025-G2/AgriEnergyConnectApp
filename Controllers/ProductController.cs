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
            var farmer = _context.Users.FirstOrDefault(u => u.Role == UserRole.Farmer);
            if (farmer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            model.FarmerId = farmer.Id;

            if (ModelState.IsValid)
            {
                _context.Products.Add(model);
                _context.SaveChanges();

                // ✅ Redirect to FarmerDashboard which expects a List<Product>
                return RedirectToAction("FarmerDashboard", "Dashboard");
            }

            return View(model); // Only return this if validation fails (and Add.cshtml expects a single Product)
        }

    }
}
