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
    public class AudiencesController : Controller
    {
        private readonly DbtesisContext _context;

        public AudiencesController(DbtesisContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var activeAudiences = await _context.Audiences
                .Where(a => a.Status == 1)
                .ToListAsync();

            return View(activeAudiences);
        }

        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audience = await _context.Audiences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audience == null)
            {
                return NotFound();
            }

            return View(audience);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Latitude,Longitude,Name, Direction, Image")] Audience audience)
        {
            if (await IsNameDuplicated(audience.Name))
            {
                ModelState.AddModelError(nameof(audience.Name), "There is already an auditorium with the same name.");
            }
           
            if (!decimal.TryParse(audience.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture), out var latitude) ||
            !decimal.TryParse(audience.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture), out var longitude))
            {
                ModelState.AddModelError("", "Invalid coordinates format.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(audience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(audience);
        }
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audience = await _context.Audiences.FindAsync(id);
            if (audience == null)
            {
                return NotFound();
            }
            return View(audience);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Latitude,Longitude,Name,Status, Direction, Image")] Audience audience)
        {

            if (id != audience.Id)
            {
                return NotFound();
            }

            if (!decimal.TryParse(audience.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture), out var latitude) ||
            !decimal.TryParse(audience.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture), out var longitude))
            {
                ModelState.AddModelError("", "Invalid coordinates format.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AudienceExists(audience.Id))
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
            return View(audience);
        }

        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audience = await _context.Audiences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audience == null)
            {
                return NotFound();
            }

            return View(audience);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var audience = await _context.Audiences.FindAsync(id);
            if (audience != null)
            {
                _context.Audiences.Remove(audience);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AudienceExists(int id)
        {
            return _context.Audiences.Any(e => e.Id == id);
        }

        private async Task<bool> IsNameDuplicated(string name)
        {
            return await _context.Audiences.AnyAsync(a => a.Name == name);
        }

        private async Task<bool> IsLocationDuplicated(string latitude, string longitude)
        {
            return await _context.Audiences.AnyAsync(a => a.Latitude == latitude && a.Longitude == longitude);
        }

    }
}
