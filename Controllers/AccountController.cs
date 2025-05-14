using AgriEnergyConnectApp.Data;
using AgriEnergyConnectApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Required for session

namespace AgriEnergyConnectApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Register
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = model.Password, 
                    Role = model.Role
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }

        // GET: Login
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                // ✅ Store the UserId in session
                HttpContext.Session.SetInt32("UserId", user.Id);

                if (user.Role == UserRole.Farmer)
                    return RedirectToAction("FarmerDashboard", "Dashboard");

                if (user.Role == UserRole.Employee)
                    return RedirectToAction("EmployeeDashboard", "Dashboard");
            }

            ModelState.AddModelError("", "Invalid credentials");
            return View(model);
        }
    }
}
