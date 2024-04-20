﻿using Microsoft.EntityFrameworkCore;
using Vendre_pieces_auto.Models.Tabels;
namespace Vendre_pieces_auto.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) {
        
        }
        public DbSet<Vendeur> Vendeur { get; set; }
        public DbSet<Acheteur> Acheteur { get; set;}
        public DbSet<Controleur> controleur { get; set; }
        public DbSet<Piece> Piece { get; set; }
        public DbSet<Commande> Commande { get; set; }
        public DbSet<Facture> Facture { get; set; }
    }
}