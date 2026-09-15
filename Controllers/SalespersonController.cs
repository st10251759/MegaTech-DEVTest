using DEVTEST_Cameron_MVC.Data;
using DEVTEST_Cameron_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DEVTEST_Cameron_MVC.Controllers
{
    public class SalespersonController : Controller
    {
        // context is injected through the constructor so the controller
        // does not create its own database connection
        private readonly ApplicationDbContext _context;

        public SalespersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // shows every salesperson in a table
        public async Task<IActionResult> Index()
        {
            List<Salesperson> salespersonList = await _context.Salespersons.ToListAsync();
            return View(salespersonList);
        }

        // shows one salesperson's full details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Salesperson salesperson = await _context.Salespersons
                .FirstOrDefaultAsync(item => item.Id == id);

            if (salesperson == null)
            {
                return NotFound();
            }

            return View(salesperson);
        }

        // returns the empty create form
        public IActionResult Create()
        {
            return View();
        }

        // receives the submitted form and saves a new salesperson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Salary")] Salesperson salesperson)
        {
            // model validation runs the data annotations set on the model
            // (Required, StringLength, Range) before anything is saved
            if (ModelState.IsValid)
            {
                _context.Add(salesperson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(salesperson);
        }

        // returns the edit form filled with the current values
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Salesperson salesperson = await _context.Salespersons.FindAsync(id);

            if (salesperson == null)
            {
                return NotFound();
            }

            return View(salesperson);
        }

        // receives the edited form and updates the record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Salary")] Salesperson salesperson)
        {
            if (id != salesperson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesperson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // check whether the record was deleted by someone else
                    // while this edit was in progress
                    bool stillExists = await _context.Salespersons.AnyAsync(item => item.Id == salesperson.Id);
                    if (!stillExists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(salesperson);
        }

        // shows a confirmation page before deleting
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Salesperson salesperson = await _context.Salespersons
                .FirstOrDefaultAsync(item => item.Id == id);

            if (salesperson == null)
            {
                return NotFound();
            }

            return View(salesperson);
        }

        // runs after the confirmation page and removes the record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Salesperson salesperson = await _context.Salespersons.FindAsync(id);

            if (salesperson != null)
            {
                _context.Salespersons.Remove(salesperson);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}