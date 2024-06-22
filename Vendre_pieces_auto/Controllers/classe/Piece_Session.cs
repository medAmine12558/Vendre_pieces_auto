using Vendre_pieces_auto.Models.Tabels;

namespace Vendre_pieces_auto.Controllers.classe
{
    public class Piece_Session
    {
        public int Id_piece { get; set; }
        public string Nom_piece { get; set; }
        public string Type_name { get; set; }
        public string Id_Vendeur { get; set; }
        public int Quantite_stock { get; set; }
        public bool is_valide { get; set; }
        public float prix { get; set; }
        public float prixtotal { get; set; }
        public virtual ICollection<Photos> Photos { get; set; }
        public int Quantite_acheter;
        public Piece_Session()
        {
            is_valide = false;
        }
    }
}
