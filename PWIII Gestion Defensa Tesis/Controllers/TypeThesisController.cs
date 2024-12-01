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
    public class TypeThesisController : Controller
    {
        private readonly DbtesisContext _context;

        public TypeThesisController(DbtesisContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeTheses.ToListAsync());
        }

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] TypeThesis typeThesis)
        {
            if (await IsNameDuplicated(typeThesis.Name, typeThesis.Id))
            {
                ModelState.AddModelError(nameof(typeThesis.Name), "There is already an Type Thesis with the same name.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(typeThesis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeThesis);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Name,Description")] TypeThesis typeThesis)
        {

            if (id != typeThesis.Id)
            {
                return NotFound();
            }

            if (await IsNameDuplicated(typeThesis.Name, typeThesis.Id))
            {
                ModelState.AddModelError(nameof(typeThesis.Name), "There is already an Type Thesis with the same name.");
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var typeThesis = await _context.TypeTheses.FindAsync(id);
            if (typeThesis != null)
            {
                _context.TypeTheses.Remove(typeThesis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeThesisExists(byte id)
        {
            return _context.TypeTheses.Any(e => e.Id == id);
        }
        private async Task<bool> IsNameDuplicated(string name, int? currentId = null)
        {
            return await _context.TypeTheses
                .AnyAsync(a => a.Name == name && (!currentId.HasValue || a.Id != currentId.Value));
        }
    }
}
