namespace Vendre_pieces_auto.Models.DAO
{
    public class PieceNoValide
    {
        public int Id_piece { get; set; }
        public string Nom_piece { get; set; }
        public string Type_name { get; set; }
        public string Id_Vendeur { get; set; }
        public float prix { get; set; }
        public List<string> NomCinVendeur=new List<string>() ;
    }
}
