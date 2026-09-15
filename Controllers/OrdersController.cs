using DEVTEST_Cameron_MVC.Data;
using DEVTEST_Cameron_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DEVTEST_Cameron_MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 6.1 - shows every order along with the customer and salesperson names
        public async Task<IActionResult> Index()
        {
            List<Order> orderList = await _context.Orders
                .Include(item => item.Customer)
                .Include(item => item.Salesperson)
                .ToListAsync();

            return View(orderList);
        }

        // shows one order's full details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders
                .Include(item => item.Customer)
                .Include(item => item.Salesperson)
                .FirstOrDefaultAsync(item => item.SalesOrder == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // 6.2 - returns the create form with customer and salesperson dropdowns filled in
        public IActionResult Create()
        {
            ViewBag.CustId = new SelectList(_context.Customers, "Id", "Name");
            ViewBag.SalesPersonId = new SelectList(_context.Salespersons, "Id", "Name");
            return View();
        }

        // 6.2 - receives the submitted form and saves a new order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesOrder,OrderDate,CustId,SalesPersonId,Amount")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // dropdowns need to be rebuilt if validation fails,
            // otherwise the form reloads with empty lists
            ViewBag.CustId = new SelectList(_context.Customers, "Id", "Name", order.CustId);
            ViewBag.SalesPersonId = new SelectList(_context.Salespersons, "Id", "Name", order.SalesPersonId);
            return View(order);
        }

        // returns the edit form with current values and dropdowns
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            ViewBag.CustId = new SelectList(_context.Customers, "Id", "Name", order.CustId);
            ViewBag.SalesPersonId = new SelectList(_context.Salespersons, "Id", "Name", order.SalesPersonId);
            return View(order);
        }

        // receives the edited form and updates the record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesOrder,OrderDate,CustId,SalesPersonId,Amount")] Order order)
        {
            if (id != order.SalesOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool stillExists = await _context.Orders.AnyAsync(item => item.SalesOrder == order.SalesOrder);
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

            ViewBag.CustId = new SelectList(_context.Customers, "Id", "Name", order.CustId);
            ViewBag.SalesPersonId = new SelectList(_context.Salespersons, "Id", "Name", order.SalesPersonId);
            return View(order);
        }

        // shows a confirmation page before deleting
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders
                .Include(item => item.Customer)
                .Include(item => item.Salesperson)
                .FirstOrDefaultAsync(item => item.SalesOrder == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // runs after the confirmation page and removes the record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Order order = await _context.Orders.FindAsync(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}