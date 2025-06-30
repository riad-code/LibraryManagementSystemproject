using System.Diagnostics;
using LIBRARYMANAGEMENT.Data;
using LIBRARYMANAGEMENT.Models;
using LIBRARYMANAGEMENT.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIBRARYMANAGEMENT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> BookList()
        {
            var books = await _context.Books.Include(b => b.Category).ToListAsync();
            return View(books);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestBook(int bookId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var request = new BookRequest
            {
                BookID = bookId,
                UserID = user.Id,
                RequestDate = DateTime.Now,
                Status = "Pending"
            };

            _context.BookRequests.Add(request);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Book request submitted.";
            return RedirectToAction("BookList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(int bookId, string subject, string messageBody)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var message = new UserMessage
            {
                BookID = bookId,
                UserID = user.Id,
                Subject = subject,
                MessageBody = messageBody,
                SentAt = DateTime.Now
            };

            _context.UserMessages.Add(message);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Message sent to admin.";
            return RedirectToAction("BookList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
