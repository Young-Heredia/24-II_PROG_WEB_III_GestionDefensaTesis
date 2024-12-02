using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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

        [HttpPost]
        public async Task<IActionResult> Index(User currentUser)
        {
            var userLogin = from _user in _context.Users
                            join _role in _context.Rols on _user.RoleId equals _role.RoleId
                            where _user.Email == currentUser.Email && _user.Password == currentUser.Password
                            select new
                            {
                                UserId = _user.UserId,
                                UserEmail = _user.Email,
                                UserPassword = _user.Password,
                                UserName = _user.UserName,
                                UserStatus = _user.Status,
                                UserRoleName = _role.RolName,
                            };

            if (userLogin.Count() != 0)
            {
                string role = userLogin.First().UserRoleName;
                string email = userLogin.First().UserEmail;

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,"User"),
                    new Claim("Email",email),
                    new Claim(ClaimTypes.Role,role)
                };

                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Message"] = "Invalid email or password";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

    }
}




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