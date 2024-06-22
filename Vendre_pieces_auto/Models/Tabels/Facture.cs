using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vendre_pieces_auto.Models.Tabels
{
    public class Facture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Id_Comm { get; set; }
        [ForeignKey("Id_Comm")]
        public Commande Commandee { get; set; }
    }
}
