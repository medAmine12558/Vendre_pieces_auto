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
        private void ajouter_panier_function(int id,int q)
        {
            List<Piece> panier = HttpContext.Session.Get<List<Piece>>("panier");
            if (panier == null)
            {
                panier = new List<Piece>();
            }
            var piece = _context.Piece.Include(p => p.Photos).SingleOrDefault(p => p.Id_piece == id);
            piece.Quantite_stock = q;
            panier.Add(piece);
            foreach (var p in panier)
                Console.WriteLine(p.Nom_piece);

            HttpContext.Session.Set("panier", panier);

        }
        [HttpGet]
        public IActionResult ajouter_panier(int id,int q)
        {
            Console.WriteLine("le id est : "+id);
            var pieces = _context.Piece.Include(p => p.Photos).ToList();

            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, RedirectUrl = Url.Action("Panier_Auth", "Autho", new { id = id, q = q }) });
            } 
            if (id != null)
            {
                ajouter_panier_function(id, q);
                return Json(new { success = true });
            
            }
            else
            {
                
                return Json(new { success = false });
            }
        }
        public IActionResult redirection_apres_auth(int id, int q)
        {
            ajouter_panier_function(id, q);
            return RedirectToAction("InterfaceUser","User_Interface");


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

