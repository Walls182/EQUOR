using EQUOR.Logica;
using EQUOR.Models;
using Microsoft.AspNetCore.Mvc;

namespace EQUOR.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Consumer _consumer)
        {
            Da_Logica _da_Consumer = new Da_Logica();

            var Consumer = _da_Consumer.ValidarUsuario(_consumer.Email, _consumer.Password);

            if(Consumer != null) {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        public IActionResult Index(Company _company)
        {
            Da_Logica _da_Company = new Da_Logica();

            var Company = _da_Company.ValidarCompany(_company.Email, _company.Password);

            if (Company != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }
        public IActionResult CerrarSesion()
        {
            return RedirectToAction("Index", "Acceso");

        }
    }
}
