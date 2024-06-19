using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Vendre_pieces_auto.Data;
using Vendre_pieces_auto.Models.Tabels;

namespace Vendre_pieces_auto.Controllers
{
    public class User_InterfaceController : Controller
    {
        private readonly Context _context; // Ajoutez une variable privée pour stocker le contexte

        public User_InterfaceController(Context context)
        {
            _context = context; // Injectez le contexte dans le constructeur
        }

        public IActionResult InterfaceUser()
        {
            //var pieces=_context.Piece.ToList();
            var pieces = _context.Piece.Include(p => p.Photos).ToList();
            String pictureUrl = null;
            if (User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;//recuperer le uid d'utilsateur connecter
                pictureUrl=HttpContext.User.FindFirst("picture")?.Value;


                if (_context.Controleur.Any(c => c.Id == userId)) {//verifier est ce que le id de l'utilisateur connecter est ce qu'il est enregistrer dans la base de donnees des controlleur , c'est une methode pour que mon systeme soit capable de savoir automatiquement si le user connecter est un controlleur ou non
                    Console.WriteLine("le id est trouve");
                }
                else
                {
                    Console.WriteLine("le id n'est pas trouve");
                }
            }
            return View(new { pieces = pieces , pictureUrl = pictureUrl });
        }
        public IActionResult Ajouter_Piece()//cette methode verifie lorsque le user veux ajouter une piece est ce qu'il est authentifie ou non
        {
            if(User.Identity.IsAuthenticated)//si user authentifie je vais le rederiger vers la page d'ajout de piece
            return View();
            else// si non je vais le rederiger vers la page d'autentifier 
            {
                return RedirectToAction("Login_Piece", "Autho");//ici on a appeler le controleur "AuthoController" avec la methode "Login_Piece" qui va lui meme appeler le processus d'auth
            }
        }
        [HttpGet]
        public IActionResult CategorieSelected(string categorie)
        {
           
            var cat=_context.Piece.Include(x => x.Photos).Where(p => p.Type_name.Equals(categorie));//recuperer toutes les produits ayant le meme categorie passe dans le url
            ViewBag.SelectedCategorie = categorie;
            return View("~/Views/User_Interface/InterfaceUser.cshtml", cat);
        }
        public IActionResult PageProfile()
        {
            var email=HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var picture = HttpContext.User.FindFirst("picture")?.Value;
            return View("~/Views/User_Interface/InterfaceProfile.cshtml",new {email=email,picture=picture});
        }
    }
    
}
