using Microsoft.AspNetCore.Mvc;

namespace Vendre_pieces_auto.Controllers
{
    public class navbarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
