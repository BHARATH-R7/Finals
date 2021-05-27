using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finals.Data;
using Finals.Models;

namespace Finals.Controllers
{
    public class QueriesController : Controller
    {
        private readonly AppdbContext _context;

        public QueriesController(AppdbContext context)
        {
            _context = context;
        }

        //GET: Queries
        //public async Task<IActionResult> Index()
        //{


        //}

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var getQueries =  _context.Query.Include(j => j.category).Include(k => k.employee).Select(item=>item);

           

            if (!String.IsNullOrEmpty(searchString))
            {
                getQueries = getQueries.Where(e => e.employee.EMail.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    getQueries = getQueries.OrderByDescending(s => s.QueryID);
                    break;
                default:
                    getQueries = getQueries.OrderBy(s => s.QueryID);
                    break;
            }
            int pageSize = 4;
            return View(await PaginatedList<Query>.CreateAsync(getQueries.AsNoTracking(), pageNumber ?? 1, pageSize));

        }






        // GET: Queries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _context.Query
                .Include(q => q.category)
                .Include(q => q.employee)
                .FirstOrDefaultAsync(m => m.QueryID == id);
            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }

        // GET: Queries/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CatName");
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EMail");
            return View();
        }

        // POST: Queries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QueryID,description,Qstate,EmployeeID,CategoryId,Scontent")] Query query)
        {
            if (ModelState.IsValid)
            {
                _context.Add(query);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CatName", query.CategoryId);
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EMail", query.EmployeeID);
            return View(query);
        }

        // GET: Queries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _context.Query.FindAsync(id);
            if (query == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CatName", query.CategoryId);
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EMail", query.EmployeeID);
            return View(query);
        }

        // POST: Queries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QueryID,description,Qstate,EmployeeID,CategoryId,Scontent")] Query query)
        {
            if (id != query.QueryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(query);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QueryExists(query.QueryID))
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
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CatName", query.CategoryId);
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EMail", query.EmployeeID);
            return View(query);
        }

        // GET: Queries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _context.Query
                .Include(q => q.category)
                .Include(q => q.employee)
                .FirstOrDefaultAsync(m => m.QueryID == id);
            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }

        // POST: Queries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var query = await _context.Query.FindAsync(id);
            _context.Query.Remove(query);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QueryExists(int id)
        {
            return _context.Query.Any(e => e.QueryID == id);
        }
    }
}
