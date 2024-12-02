﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index(string filter = "active", string searchQuery = "")
        {
            IQueryable<Audience> audienceQuery = _context.Audiences;

 
            if (filter == "active")
            {
                audienceQuery = audienceQuery.Where(a => a.Status == 1);
            }
            else if (filter == "inactive")
            {
                audienceQuery = audienceQuery.Where(a => a.Status == 0);
            }

  
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                audienceQuery = audienceQuery.Where(a =>
                    EF.Functions.Like(a.Name ?? "", $"%{searchQuery}%") ||
                    EF.Functions.Like(a.Direction ?? "", $"%{searchQuery}%"));
            }

            var audiences = await audienceQuery.ToListAsync();
            ViewBag.Filter = filter;
            ViewBag.SearchQuery = searchQuery;

            return View(audiences);
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
        public async Task<IActionResult> Create([Bind("Id,Latitude,Longitude,Name,Direction,Image")] Audience audience)
        {
            if (await IsNameDuplicated(audience.Name))
            {
                ModelState.AddModelError(nameof(audience.Name), "There is already an auditorium with the same name.");
            }

            if (ModelState.IsValid)
            {
                audience.Status = 1; 
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
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Latitude,Longitude,Name,Status,Direction,Image")] Audience audience)
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
                audience.Status = 0; 
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(byte id)
        {
            var audience = await _context.Audiences.FindAsync(id);
            if (audience == null)
            {
                return NotFound();
            }

            audience.Status = 1; 
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { filter = "inactive" });
        }

        private bool AudienceExists(byte id)
        {
            return _context.Audiences.Any(e => e.Id == id);
        }

        private async Task<bool> IsNameDuplicated(string name)
        {
            return await _context.Audiences.AnyAsync(a => a.Name == name);
        }
    }
}
