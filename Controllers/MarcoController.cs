using Microsoft.AspNetCore.Mvc;
using controle_de_pontos_mvc.Models;
using System.Diagnostics;


namespace controle_de_pontos_mvc.Controllers
{
    public class MarcoController : Controller
    {
        public IActionResult MarcoDispesas()
        {

            return View();
        }
    }
}
