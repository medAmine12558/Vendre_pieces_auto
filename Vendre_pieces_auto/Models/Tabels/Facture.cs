using System.ComponentModel.DataAnnotations.Schema;

namespace Vendre_pieces_auto.Models.Tabels
{
    public class Facture
    {
        public Guid Id { get; set; }
        [ForeignKey("Id_Vendeur")]
        public Vendeur Vendeur { get; set; }
        [ForeignKey("Id_Acheteur")]
        public Acheteur Acheteur { get; set; }
        [ForeignKey("Id_Commande")]
        public Commande Commande {  get; set; }


    }
}
