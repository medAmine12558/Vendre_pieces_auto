using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
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
                return Json(new { success = false, RedirectUrl = Url.Action("Panier_Auth", "Autho", new { id = id }) });

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

                    HttpContext.Session.Set("panier", panier);
                    return Json(new { success = true });
                }
                return Json(new { success = true });
            }
        }
        public IActionResult page_panier()
        {
            float somme = 0;
            List<Piece> p = HttpContext.Session.Get<List<Piece>>("panier");
            if (p == null)
            {
                return View();
            }
            else
            {
                foreach (var piece in p)
                {
                    somme = somme + piece.prix;
                }
                return View(new { p = p, somme = somme });
            }
        }

        public IActionResult page_confirmation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult remove_piece(int id)
        {
            List<Piece> panier = HttpContext.Session.Get<List<Piece>>("panier");
            if (panier != null)
            {
                Piece p = panier.FirstOrDefault(i => i.Id_piece == id);
                if (p != null)
                {
                    panier.Remove(p);
                    HttpContext.Session.Set("panier", panier);
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });



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
}

