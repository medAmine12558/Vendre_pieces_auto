using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendre_pieces_auto.Models.Tabels
{
    public class Photos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id_image { get; set; }
        [ForeignKey("id_Piece")]
        public Piece Piece { get; set; }
        public string image { get; set; }
    }
}
