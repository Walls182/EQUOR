using EQUOR.Models;
using EQUOR.Logica_login;
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
        public IActionResult Index(string correo, string clave)
        {
            Consumer objeto = new Logica_Login().EncontrarConsumer(correo,clave);
            if (objeto.Email != null)
            {
             
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
