using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWIII_Gestion_Defensa_Tesis.Models;
using PWIII_Gestion_Defensa_Tesis.Data;

namespace PWIII_Gestion_Defensa_Tesis.Controllers
{
    public class ProfessionalController : Controller
    {
        private readonly DbtesisContext _context;

        public ProfessionalController(DbtesisContext context)
        {
            _context = context;
        }

        // GET: Professional
        public async Task<IActionResult> Index(string filter = "active")
        {
            IQueryable<Professional> professionalsQuery = _context.Professionals;

            // Filtrar según el parámetro recibido
            if (filter == "active")
            {
                professionalsQuery = professionalsQuery.Where(p => p.Status == 1);
            }
            else if (filter == "inactive")
            {
                professionalsQuery = professionalsQuery.Where(p => p.Status == 0);
            }

            var professionals = await professionalsQuery
                .Include(p => p.ActivityProfessionals)
                .ThenInclude(ap => ap.IdActivityNavigation)
                .ToListAsync();

            ViewBag.Filter = filter; // Para mantener el estado del filtro en la vista
            return View(professionals);
        }


        // GET: Professional/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professional = await _context.Professionals
                .Include(p => p.ActivityProfessionals)
                .ThenInclude(ap => ap.IdActivityNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (professional == null)
            {
                return NotFound();
            }

            return View(professional);
        }

        // GET: Professional/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professional/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,SecondLastName,Career")] Professional professional)
        {
            if (ModelState.IsValid)
            {
                professional.Status = 1; // Estado por defecto: Activo
                _context.Add(professional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professional);
        }


        // GET: Professional/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professional = await _context.Professionals.FindAsync(id);
            if (professional == null)
            {
                return NotFound();
            }
            return View(professional);
        }

        // POST: Professional/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name,LastName,SecondLastName,Career")] Professional professional)
        {
            if (id != professional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingProfessional = await _context.Professionals.FindAsync(id);
                    if (existingProfessional != null)
                    {
                        existingProfessional.Name = professional.Name;
                        existingProfessional.LastName = professional.LastName;
                        existingProfessional.SecondLastName = professional.SecondLastName;
                        existingProfessional.Career = professional.Career;
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessionalExists(professional.Id))
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

            return View(professional);
        }


        // GET: Professional/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professional = await _context.Professionals
                .FirstOrDefaultAsync(m => m.Id == id);

            if (professional == null)
            {
                return NotFound();
            }

            return View(professional);
        }

        // POST: Professional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var professional = await _context.Professionals.FindAsync(id);
            if (professional != null)
            {
                professional.Status = 0; // Cambiar a Inactivo
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        private bool ProfessionalExists(short id)
        {
            return _context.Professionals.Any(e => e.Id == id);
        }
    }
}
