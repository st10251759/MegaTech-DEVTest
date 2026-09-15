using DEVTEST_Cameron_MVC.Data;
using DEVTEST_Cameron_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEVTEST_Cameron_MVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // shows every customer in a table
        public async Task<IActionResult> Index()
        {
            List<Customer> customerList = await _context.Customers.ToListAsync();
            return View(customerList);
        }

        // shows one customer's full details, including their orders
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = await _context.Customers
                .Include(item => item.Orders)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // returns the empty create form
        public IActionResult Create()
        {
            return View();
        }

        // receives the submitted form and saves a new customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,City,IndustryType")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // returns the edit form filled with the current values
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // receives the edited form and updates the record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City,IndustryType")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool stillExists = await _context.Customers.AnyAsync(item => item.Id == customer.Id);
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

            return View(customer);
        }

        // shows a confirmation page before deleting
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = await _context.Customers
                .FirstOrDefaultAsync(item => item.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // runs after the confirmation page and removes the record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}