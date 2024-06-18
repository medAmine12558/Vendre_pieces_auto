using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Vendre_pieces_auto.Models.Tabels
{
    public class Commander
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Commande_id { get; set; } // Clé étrangère pour Commande
        [ForeignKey("Commande_id")]
        public Commande Commande { get; set; }

        public int Piece_id { get; set; } // Clé étrangère pour Piece
        [ForeignKey("Piece_id")]
        public Piece Piece { get; set; }

        public int quantite { get; set; }
    }
}
