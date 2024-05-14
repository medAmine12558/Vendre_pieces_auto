using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Vendre_pieces_auto.Data;

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
            var pieces=_context.Piece.ToList();
               
            if (User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;//recuperer le uid d'utilsateur connecter
                if(_context.Controleur.Any(c => c.Id == userId)) {//verifier est ce que le id de l'utilisateur connecter est ce qu'il est enregistrer dans la base de donnees des controlleur , c'est une methode pour que mon systeme soit capable de savoir automatiquement si le user connecter est un controlleur ou non
                    Console.WriteLine("le id est trouve");
                }
                else
                {
                    Console.WriteLine("le id n'est pas trouve");

                }
            }
            return View(pieces);
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
    }
    
}
