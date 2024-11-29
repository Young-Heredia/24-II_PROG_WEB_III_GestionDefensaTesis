using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PWIII_Gestion_Defensa_Tesis.Data;
using PWIII_Gestion_Defensa_Tesis.Models;
using System.Security.Claims;

namespace PWIII_Gestion_Defensa_Tesis.Controllers
{
    public class LoginController : Controller
    {
        private readonly DbtesisContext _context;

        public LoginController(DbtesisContext context)
        {
            _context = context;
        }














        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        /*[HttpPost]
        public async Task<IActionResult> Index(User currentuser)
        {
            var userLogin = from user in _context.Users
                            where user.Email == currentuser.Email && user.Password == currentuser.Password
                            select new
                            {
                                UserId = currentuser.Id,
                                UserRole = currentuser.Role,
                                UserStatus = currentuser.Status,
                                UserPassword = currentuser.Password,
                                UserName = currentuser.UserName,
                                UserEmail = currentuser.Email,
                            };

            if (userLogin.Any())
            {
                var user = userLogin.First();
                var claims = new List<Claim>
                {
                    /*new Claim(ClaimTypes.User, user.Email),
                    new Claim(ClaimTypes.Role, user.RolName)*/
                /*;

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            }
            else
            {
                ViewData["Mensaje"] = "Invalid email or password";
                //ViewData View();
            }*/
























        /*// GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }

}