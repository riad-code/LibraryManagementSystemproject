using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LIBRARYMANAGEMENT.Models; // if you have a custom ApplicationUser class

namespace LIBRARYMANAGEMENT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("AdminDashboard")]
        public IActionResult Dashboard()
        {
            var users = _userManager.Users.ToList(); // ✅ fetch all users
            return View(users); // ✅ pass them to the Dashboard view
        }
    }
}
