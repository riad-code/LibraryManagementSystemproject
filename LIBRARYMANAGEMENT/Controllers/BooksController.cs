using LIBRARYMANAGEMENT.Data;
using LIBRARYMANAGEMENT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LIBRARYMANAGEMENT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.Category)
                .ToListAsync();
            return View(books);
        }

        // GET: /Books/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: /Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName", book.CategoryID);
            return View(book);
        }

        // GET: /Books/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName", book.CategoryID);
            return View(book);
        }

        // POST: /Books/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName", book.CategoryID);
            return View(book);
        }

        // GET: /Books/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
