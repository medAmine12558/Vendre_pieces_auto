using Microsoft.AspNetCore.Mvc;

namespace Vendre_pieces_auto.Controllers
{
    public class User_InterfaceController : Controller
    {
        public IActionResult InterfaceUser()
        {
            return View();
        }
    }
}
