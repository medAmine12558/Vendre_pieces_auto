using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendre_pieces_auto.Models.Tabels
{
    public class Piece
    {
        [Key]
        public  Guid Id_piece { get; set; }
        public string Nom_piece { get; set; }
        public string Type_name { get; set; }
        [ForeignKey("Id_vendeur")]
        public Vendeur Vendeur { get; set; }
        public int Quantite_stock { get; set; }
    }
}
