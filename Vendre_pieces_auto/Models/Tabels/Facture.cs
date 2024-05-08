using System.ComponentModel.DataAnnotations.Schema;

namespace Vendre_pieces_auto.Models.Tabels
{
    public class Facture
    {
        public Guid Id { get; set; }
        
        public string VendeurId { get; set; }
        
        public string AcheteurId { get; set; }
        [ForeignKey("Id_Commande")]
        public Commande Commande {  get; set; }


    }
}
