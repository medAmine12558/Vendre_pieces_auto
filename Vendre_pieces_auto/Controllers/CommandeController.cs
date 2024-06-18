using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vendre_pieces_auto.Data;
using Vendre_pieces_auto.Models.Tabels;

namespace Vendre_pieces_auto.Controllers
{
    public class CommandeController : Controller
    {
        private readonly Context _context;
        public CommandeController(Context context) {
            this._context = context;
        }
        public IActionResult ajouter_comm()
        {
            List<Piece> panier = HttpContext.Session.Get<List<Piece>>("panier");
            foreach (Piece piece in panier) {
                var comm = new Commande
                {
                    Id_Vendeur = piece.Id_Vendeur,
                    Id_Acheteur = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                };
                _context.Commande.Add(comm);
                _context.SaveChanges();


                Console.WriteLine(comm.Id);
                var commander = new Commander
                {
                    Commande_id = comm.Id,
                    Piece_id = piece.Id_piece,
                    
                    quantite = piece.Quantite_stock
                };
                _context.Commander.Add(commander);
                var f = new Facture
                {
                    Id_Vendeur = piece.Id_Vendeur,
                    Id_Acheteur = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    Id_Comm = comm.Id
                };
                _context.Facture.Add(f);
            }
            _context.SaveChanges();
            return Json(new { success = true });
        }
    }
}
