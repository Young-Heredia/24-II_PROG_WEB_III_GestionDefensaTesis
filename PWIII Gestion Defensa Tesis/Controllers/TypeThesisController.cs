using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWIII_Gestion_Defensa_Tesis.Models;
using PWIII_Gestion_Defensa_Tesis.Data;

namespace PWIII_Gestion_Defensa_Tesis.Controllers
{
    public class TypeThesisController : Controller
    {
        private readonly DbtesisContext _context;

        public TypeThesisController(DbtesisContext context)
        {
            _context = context;
        }

        // GET: TypeThesis
        public async Task<IActionResult> Index(string filter = "active", string searchQuery = "")
        {
            IQueryable<TypeThesis> typeThesisQuery = _context.TypeTheses;

  
            if (filter == "active")
            {
                typeThesisQuery = typeThesisQuery.Where(t => t.Status == 1);
            }
            else if (filter == "inactive")
            {
                typeThesisQuery = typeThesisQuery.Where(t => t.Status == 0);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                typeThesisQuery = typeThesisQuery.Where(t =>
                    EF.Functions.Like(t.Name ?? "", $"%{searchQuery}%"));
            }

            var typeTheses = await typeThesisQuery.ToListAsync();
            ViewBag.Filter = filter;
            ViewBag.SearchQuery = searchQuery;
            return View(typeTheses);
        }

        // GET: TypeThesis/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeThesis = await _context.TypeTheses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeThesis == null)
            {
                return NotFound();
            }

            return View(typeThesis);
        }

        // GET: TypeThesis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeThesis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TypeThesis typeThesis)
        {
            if (ModelState.IsValid)
            {
                typeThesis.Status = 1;
                _context.Add(typeThesis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeThesis);
        }

        // GET: TypeThesis/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeThesis = await _context.TypeTheses.FindAsync(id);
            if (typeThesis == null)
            {
                return NotFound();
            }
            return View(typeThesis);
        }

        // POST: TypeThesis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Name")] TypeThesis typeThesis)
        {
            if (id != typeThesis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeThesis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeThesisExists(typeThesis.Id))
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
            return View(typeThesis);
        }

        // GET: TypeThesis/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeThesis = await _context.TypeTheses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeThesis == null)
            {
                return NotFound();
            }

            return View(typeThesis);
        }

        // POST: TypeThesis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var typeThesis = await _context.TypeTheses.FindAsync(id);
            if (typeThesis != null)
            {
                typeThesis.Status = 0; 
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: TypeThesis/Reactivate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(byte id)
        {
            var typeThesis = await _context.TypeTheses.FindAsync(id);
            if (typeThesis == null)
            {
                return NotFound();
            }

            typeThesis.Status = 1; 
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { filter = "inactive" });
        }

        private bool TypeThesisExists(byte id)
        {
            return _context.TypeTheses.Any(e => e.Id == id);
        }
    }
}
