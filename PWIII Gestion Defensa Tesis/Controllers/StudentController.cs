using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWIII_Gestion_Defensa_Tesis.Models;
using PWIII_Gestion_Defensa_Tesis.Data;

namespace PWIII_Gestion_Defensa_Tesis.Controllers
{
    public class StudentController : Controller
    {
        private readonly DbtesisContext _context;

        public StudentController(DbtesisContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index(string filter = "active", string searchQuery = "")
        {
            IQueryable<Student> studentsQuery = _context.Students;

            if (filter == "active")
            {
                studentsQuery = studentsQuery.Where(s => s.Status == 1);
            }
            else if (filter == "inactive")
            {
                studentsQuery = studentsQuery.Where(s => s.Status == 0);
            }


            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                studentsQuery = studentsQuery.Where(s =>
                    EF.Functions.Like(s.Name ?? "", $"%{searchQuery}%") || 
                    EF.Functions.Like(s.LastName ?? "", $"%{searchQuery}%") ||
                    EF.Functions.Like(s.SecondLastName ?? "", $"%{searchQuery}%") ||
                    EF.Functions.Like(s.ci ?? "", $"%{searchQuery}%"));
            }


            var students = await studentsQuery.Include(s => s.DefenseActivities).ToListAsync();
            ViewBag.Filter = filter; 
            ViewBag.SearchQuery = searchQuery;
            return View(students);
        }


        // GET: Student/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.DefenseActivities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,SecondLastName,ci")] Student student)
        {

            if (await IsCIDuplicated(student.ci))
            {
                ModelState.AddModelError(nameof(student.ci), "There is already an student with the same ci.");
            }
            if (ModelState.IsValid)
            {
                if (student.SecondLastName == null)
                {
                    student.SecondLastName = "Ninguno";
                }
                student.Status = 1;
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }


        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student.SecondLastName == "Ninguno")
            {
                student.SecondLastName = "";
            }
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name,LastName,SecondLastName,ci")] Student student)
        {
            if (await IsCIDuplicated(student.ci))
            {
                ModelState.AddModelError(nameof(student.ci), "There is already an student with the same ci.");
            }
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (student.SecondLastName == null)
                    {
                        student.SecondLastName = "Ninguno";
                    }
                    var existingStudent = await _context.Students.FindAsync(id);
                    if (existingStudent != null)
                    {
                        existingStudent.Name = student.Name;
                        existingStudent.LastName = student.LastName;
                        existingStudent.SecondLastName = student.SecondLastName;
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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

            return View(student);
        }


        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                student.Status = 0; 
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        private bool StudentExists(short id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        private async Task<bool> IsCIDuplicated(string ci)
        {
            return await _context.Students.AnyAsync(a => a.ci == ci);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(short id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            student.Status = 1; // Cambiar el estado a activo
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { filter = "inactive" }); // Volver a la lista de inactivos
        }
    }
}
