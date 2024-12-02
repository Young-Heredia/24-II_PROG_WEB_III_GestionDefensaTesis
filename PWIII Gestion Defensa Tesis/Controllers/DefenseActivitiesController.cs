using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PWIII_Gestion_Defensa_Tesis.Data;
using PWIII_Gestion_Defensa_Tesis.Models;

namespace PWIII_Gestion_Defensa_Tesis.Controllers
{
    public class DefenseActivitiesController : Controller
    {
        private readonly DbtesisContext _context;

        public DefenseActivitiesController(DbtesisContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dbtesisContext = _context.DefenseActivities.Include(d => d.IdAudienceNavigation).Include(d => d.IdStudentNavigation).Include(d => d.IdThesisNavigation);
            return View(await dbtesisContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defenseActivity = await _context.DefenseActivities
                .Include(d => d.IdAudienceNavigation)
                .Include(d => d.IdStudentNavigation)
                .Include(d => d.IdThesisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (defenseActivity == null)
            {
                return NotFound();
            }

            return View(defenseActivity);
        }

        public IActionResult Create()
        {
            ViewData["IdAudience"] = new SelectList(_context.Audiences
                .Select(a => new { a.Id, Name = a.Name }), "Id", "Name");

            ViewData["IdStudent"] = new SelectList(_context.Students
    .Select(s => new { s.Id, FullName = s.Name + " " + s.LastName + " " + s.SecondLastName }),
    "Id", "FullName");

            ViewData["IdThesis"] = new SelectList(_context.Theses
                .Select(t => new { t.Id, Title = t.Name }), "Id", "Title");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Status,DefenseDate,IdThesis,IdAudience,IdStudent,StatusThesis,registerDate")] DefenseActivity defenseActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(defenseActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAudience"] = new SelectList(_context.Audiences, "Id", "Direction", defenseActivity.IdAudience);
            ViewData["IdStudent"] = new SelectList(_context.Students, "Id", "LastName", defenseActivity.IdStudent);
            ViewData["IdThesis"] = new SelectList(_context.Theses, "Id", "Description", defenseActivity.IdThesis);
            return View(defenseActivity);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defenseActivity = await _context.DefenseActivities.FindAsync(id);
            if (defenseActivity == null)
            {
                return NotFound();
            }
            ViewData["IdAudience"] = new SelectList(_context.Audiences, "Id", "Direction", defenseActivity.IdAudience);
            ViewData["IdStudent"] = new SelectList(_context.Students, "Id", "LastName", defenseActivity.IdStudent);
            ViewData["IdThesis"] = new SelectList(_context.Theses, "Id", "Description", defenseActivity.IdThesis);
            return View(defenseActivity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Status,DefenseDate,IdThesis,IdAudience,IdStudent,StatusThesis,registerDate")] DefenseActivity defenseActivity)
        {
            if (id != defenseActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(defenseActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DefenseActivityExists(defenseActivity.Id))
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
            ViewData["IdAudience"] = new SelectList(_context.Audiences, "Id", "Direction", defenseActivity.IdAudience);
            ViewData["IdStudent"] = new SelectList(_context.Students, "Id", "LastName", defenseActivity.IdStudent);
            ViewData["IdThesis"] = new SelectList(_context.Theses, "Id", "Description", defenseActivity.IdThesis);
            return View(defenseActivity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defenseActivity = await _context.DefenseActivities
                .Include(d => d.IdAudienceNavigation)
                .Include(d => d.IdStudentNavigation)
                .Include(d => d.IdThesisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (defenseActivity == null)
            {
                return NotFound();
            }

            return View(defenseActivity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var defenseActivity = await _context.DefenseActivities.FindAsync(id);
            if (defenseActivity != null)
            {
                _context.DefenseActivities.Remove(defenseActivity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DefenseActivityExists(int id)
        {
            return _context.DefenseActivities.Any(e => e.Id == id);
        }
    }
}
