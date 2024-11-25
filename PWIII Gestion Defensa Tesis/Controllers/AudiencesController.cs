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
        public async Task<IActionResult> Create([Bind("Id,Latitude,Longitude,Name")] Audience audience)
        {
            // Validar duplicados
            if (await IsNameDuplicated(audience.Name))
            {
                ModelState.AddModelError(nameof(audience.Name), "There is already an auditorium with the same name.");
            }

            if (await IsLocationDuplicated(audience.Latitude, audience.Longitude))
            {
                ModelState.AddModelError(nameof(audience.Latitude), "There is already a registered auditorium at this location.");
                ModelState.AddModelError(nameof(audience.Longitude), "There is already a registered auditorium at this location.");
            }

            // Asegurar formato correcto de las coordenadas
            audience.Latitude = NormalizeCoordinate(audience.Latitude);
            audience.Longitude = NormalizeCoordinate(audience.Longitude);

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
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Latitude,Longitude,Name,Status")] Audience audience)
        {
            if (id != audience.Id)
            {
                return NotFound();
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

        private bool AudienceExists(byte id)
        {
            return _context.Audiences.Any(e => e.Id == id);
        }

        private async Task<bool> IsNameDuplicated(string name)
        {
            return await _context.Audiences.AnyAsync(a => a.Name == name);
        }

        private async Task<bool> IsLocationDuplicated(double latitude, double longitude)
        {
            return await _context.Audiences.AnyAsync(a => a.Latitude == latitude && a.Longitude == longitude);
        }

        private double NormalizeCoordinate(double coordinate)
        {
            // Si la coordenada es mayor o igual a 1000 (sin punto), la normalizamos
            if (coordinate >= 1000)
            {
                return coordinate / 1000.0;
            }
            return coordinate; // Si ya está en formato correcto, no hacemos nada
        }
    }
}
