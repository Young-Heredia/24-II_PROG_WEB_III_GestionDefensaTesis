using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Create([Bind("Id,Latitude,Longitude,Name,Direction,ImagePath")] Audience audience, IFormFile imageFile)
        {
            if (await IsNameDuplicated(audience.Name, audience.Id))
            {
                ModelState.AddModelError(nameof(audience.Name), "There is already an Auditorium with the same name.");
            }

            if (string.IsNullOrEmpty(audience.ImagePath) && (imageFile == null || imageFile.Length == 0))
            {
                ModelState.AddModelError(nameof(audience.ImagePath), "An image is required.");
            }
            else if (imageFile != null && imageFile.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError(nameof(audience.ImagePath), "Only image files (jpg, jpeg, png, gif, bmp) are allowed.");
                }
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "auditoriums");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    audience.ImagePath = "/images/auditoriums/" + fileName;
                }

                _context.Add(audience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(audience);
        }


        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit([Bind("Id,Latitude,Longitude,Name,Direction,ImagePath")] Audience audience, IFormFile? imageFile)
        {

            if (await IsNameDuplicated(audience.Name, audience.Id))
            {
                ModelState.AddModelError(nameof(audience.Name), "There is already an Auditorium with the same name.");
            }

            string fileExtension = "";
            if (imageFile != null && imageFile.Length > 0)
            {
                fileExtension = Path.GetExtension(imageFile.FileName).ToLower();  
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError(nameof(audience.ImagePath), "Only image files (jpg, jpeg, png, gif, bmp) are allowed.");
                }
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "auditoriums");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    if (!string.IsNullOrEmpty(audience.ImagePath))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "auditoriums", audience.ImagePath.Replace("/images/auditoriums/", ""));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath); 
                        }
                    }

                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    audience.ImagePath = "/images/auditoriums/" + fileName;
                }

                _context.Update(audience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(audience);
        }


        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var audience = await _context.Audiences.FindAsync(id);
            if (audience != null)
            {
                audience.Status = 0;
                _context.Update(audience);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AudienceExists(int id)
        {
            return _context.Audiences.Any(e => e.Id == id);
        }

        private async Task<bool> IsNameDuplicated(string name, int? currentId = null)
        {
            return await _context.Audiences
                .AnyAsync(a => a.Name == name && (!currentId.HasValue || a.Id != currentId.Value));
        }
    }
}
