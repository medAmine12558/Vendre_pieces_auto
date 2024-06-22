using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Vendre_pieces_auto.Models.Tabels
{
    public class CommandePiece
    {
       
        public int Commande_id { get; set; } // Clé étrangère pour Commande
        public Commande Commande { get; set; }
        public int Piece_id { get; set; }
        public Piece Pieces { get; set; }
        public int quantite { get; set; }
    }
}
