using Microsoft.EntityFrameworkCore;
using Vendre_pieces_auto.Models.Tabels;
namespace Vendre_pieces_auto.Data
{
    public class Context : DbContext
    {
       
        public Context(DbContextOptions options) : base(options) {
        
        }
        


        
        public DbSet<Piece> Piece { get; set; }
        public DbSet<Commande> Commande { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Commander> Commander { get; set; }
        public DbSet<Controlleur> Controleur { get; set; }
        public DbSet<Facture> Facture { get; set; }
        public DbSet<Favoris> Favoris { get; set; }
       

    }
}
