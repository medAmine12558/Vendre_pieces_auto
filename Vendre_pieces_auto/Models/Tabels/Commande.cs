using System.ComponentModel.DataAnnotations.Schema;

namespace Vendre_pieces_auto.Models.Tabels
{
    public class Commande
    {
        public Guid Id { get; set; }
        [ForeignKey("Acheteur.cs")]
        public Guid Id_Acheteur { get; set; }
        [ForeignKey("Vendeur.cs")]
        public Guid Id_Vendeur { get; set; }
        [ForeignKey("Piece.cs")]
        public Guid Id_Piece { get; set; }
        public int Qantite { get; set; }

    }
}
