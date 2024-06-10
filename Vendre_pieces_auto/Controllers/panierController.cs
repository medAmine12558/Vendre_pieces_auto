using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Vendre_pieces_auto.Data;
using Vendre_pieces_auto.Models.Tabels;

namespace Vendre_pieces_auto.Controllers
{
    public class panierController : Controller
    {
        Context _context;
        public panierController(Context context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult ajouter_panier(int id)
        {
            var pieces = _context.Piece.Include(p => p.Photos).ToList();
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Panier_Auth", "Autho", new { id = id });

            }
            else
            {
                if (id != null)
                {



                    List<Piece> panier = HttpContext.Session.Get<List<Piece>>("panier");

                    if (panier == null)
                    {
                        panier = new List<Piece>();
                    }
                    panier.Add(_context.Piece.Include(p => p.Photos).SingleOrDefault(p => p.Id_piece == id));
                    foreach(var piece in panier)
                    {
                        Console.WriteLine(piece.Nom_piece);
                    }
                    HttpContext.Session.Set("panier", panier);
                    return View("Views/User_Interface/InterfaceUser.cshtml", pieces);
                }
                return View("Views/User_Interface/InterfaceUser.cshtml", pieces);
            }
        }
        public IActionResult page_panier()
        {
            List<Piece> p = HttpContext.Session.Get<List<Piece>>("panier");
            return View(p);
        }

        public IActionResult page_confirmation()
        {
            return View();
        }
    }
}
public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        session.SetString(key, JsonConvert.SerializeObject(value, settings));
    }

    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
}

