using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWIII_Gestion_Defensa_Tesis.Models;
using PWIII_Gestion_Defensa_Tesis.Data;
using System.IO;

namespace PWIII_Gestion_Defensa_Tesis.Controllers
{
    public class ThesisController : Controller
    {
        private readonly DbtesisContext _context;

        public ThesisController(DbtesisContext context)
        {
            _context = context;
        }

        // GET: Thesis
        public async Task<IActionResult> Index(string filter = "active", string searchQuery = "")
        {
            IQueryable<Thesis> thesesQuery = _context.Theses.Include(t => t.IdTypeThesisNavigation);

            if (filter == "active")
            {
                thesesQuery = thesesQuery.Where(t => t.Status == 1);
            }
            else if (filter == "inactive")
            {
                thesesQuery = thesesQuery.Where(t => t.Status == 0);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                thesesQuery = thesesQuery.Where(t =>
                    EF.Functions.Like(t.Name ?? "", $"%{searchQuery}%") ||
                    EF.Functions.Like(t.Description ?? "", $"%{searchQuery}%"));
            }

            var theses = await thesesQuery.ToListAsync();
            ViewBag.Filter = filter;
            ViewBag.SearchQuery = searchQuery;
            return View(theses);
        }

        // GET: Thesis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thesis = await _context.Theses
                .Include(t => t.IdTypeThesisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thesis == null)
            {
                return NotFound();
            }

            return View(thesis);
        }

        // GET: Thesis/Create
        public IActionResult Create()
        {
            ViewData["IdTypeThesis"] = new SelectList(_context.TypeTheses, "Id", "Name");
            return View();
        }

        // POST: Thesis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Note,IdTypeThesis,FilePath")] Thesis thesis, IFormFile uploadFile)
        {
            
            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory); 
            }

            
            if (thesis.Note == 0)
            {
                thesis.Note = 0.00;
            }

           
            if (!ModelState.IsValid)
            {
                
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                
                ViewData["IdTypeThesis"] = new SelectList(_context.TypeTheses, "Id", "Name", thesis.IdTypeThesis);
                return View(thesis);
            }

            
            if (uploadFile != null && uploadFile.Length > 0)
            {
               
                var allowedExtensions = new[] { ".pdf", ".docx", ".jpg", ".jpeg", ".png", ".txt" };
                var extension = Path.GetExtension(uploadFile.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("UploadFile", "El archivo debe ser de tipo PDF, DOCX, JPG, JPEG o PNG.");
                    ViewData["IdTypeThesis"] = new SelectList(_context.TypeTheses, "Id", "Name", thesis.IdTypeThesis);
                    return View(thesis);
                }

               
                var filePath = Path.Combine(uploadDirectory, uploadFile.FileName);

              
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(stream);
                }

              
                thesis.FilePath = "/uploads/" + uploadFile.FileName;
            }

        
            if (string.IsNullOrEmpty(thesis.FilePath))
            {
                thesis.FilePath = "Sin Thesis";
            }

          
            thesis.Status = 1;

            
            _context.Add(thesis);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Thesis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thesis = await _context.Theses.FindAsync(id);
            if (thesis == null)
            {
                return NotFound();
            }

            ViewData["IdTypeThesis"] = new SelectList(_context.TypeTheses, "Id", "Name", thesis.IdTypeThesis);
            return View(thesis);
        }

        // POST: Thesis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Note,IdTypeThesis,FilePath")] Thesis thesis, IFormFile uploadFile)
        {
            if (id != thesis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    if (uploadFile != null && uploadFile.Length > 0)
                    {
                        var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                        if (!Directory.Exists(uploadDirectory))
                        {
                            Directory.CreateDirectory(uploadDirectory);
                        }

                        var filePath = Path.Combine(uploadDirectory, uploadFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await uploadFile.CopyToAsync(stream);
                        }

                      
                        thesis.FilePath = "/uploads/" + uploadFile.FileName;
                    }

                   
                    _context.Update(thesis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThesisExists(thesis.Id))
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
            ViewData["IdTypeThesis"] = new SelectList(_context.TypeTheses, "Id", "Name", thesis.IdTypeThesis);
            return View(thesis);
        }

        // GET: Thesis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thesis = await _context.Theses
                .Include(t => t.IdTypeThesisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (thesis == null)
            {
                return NotFound();
            }

            return View(thesis);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var theses = await _context.Theses.FindAsync(id);
            if (theses != null)
            {
                theses.Status = 0;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var student = await _context.Theses.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            student.Status = 1; 
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { filter = "inactive" }); 
        } 

        private bool ThesisExists(int id)
        {
            return _context.Theses.Any(e => e.Id == id);
        }
    }
}
