using Microsoft.EntityFrameworkCore;
using Vendre_pieces_auto.Models.Tabels;
namespace Vendre_pieces_auto.Data
{
    public class Context : DbContext
    {
       
        public Context(DbContextOptions options) : base(options) {

        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommandePiece>().
                HasKey(pc => new { pc.Piece_id, pc.Commande_id });
            modelBuilder.Entity<CommandePiece>().
                HasOne(pc => pc.Pieces).
                WithMany(p => p.Commanders).HasForeignKey(p => p.Piece_id);
            modelBuilder.Entity<CommandePiece>().
                HasOne(x => x.Commande).WithMany(x => x.Commanders).HasForeignKey(p => p.Commande_id);

        }



  
        public DbSet<Piece> Piece { get; set; }
        public DbSet<Commande> Commande { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<CommandePiece> CommandePiece { get; set; }
        public DbSet<Controlleur> Controleur { get; set; }
        public DbSet<Facture> Facture { get; set; }
        public DbSet<Favoris> Favoris { get; set; }
       

    }
}
