using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LIBRARYMANAGEMENT.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        [Route("AdminDashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }


}
