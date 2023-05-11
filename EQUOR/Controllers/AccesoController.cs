using EQUOR.Logica;
using EQUOR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace EQUOR.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Consumer _consumer, Company _company, Manager _manager)
        {
            Da_Logica _da_Consumer = new Da_Logica();

            var Consumer = _da_Consumer.ValidarUsuario(_consumer.Email, _consumer.Password);
            var Company = _da_Consumer.ValidarCompany(_company.Email, _company.Password);
            var Manager = _da_Consumer.ValidarManager(_manager.Email, _manager.Password);

            if (Consumer != null && Company == null && Manager==null)
            {
                var claims = new List<Claim>
        {
                 new Claim(ClaimTypes.Name, Consumer.Email),
                 new Claim(ClaimTypes.Role, Consumer.IdRole.ToString())
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                );

                return RedirectToAction("Index", "Home");
            }else if (Consumer == null && Manager == null && Company != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Company.Email),
            new Claim(ClaimTypes.Role, Company.IdRole.ToString())
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                );

                return RedirectToAction("Index", "Home");
            }
            else if (Consumer == null && Manager != null && Company == null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Manager.Email),
            new Claim(ClaimTypes.Role, Manager.IdRole.ToString())
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                );


                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        


        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Acceso");

        }
    }
}
