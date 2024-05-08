﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendre_pieces_auto.Models.Tabels
{
    public class Piece
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Id_piece { get; set; }
        public string Nom_piece { get; set; }
        public string Type_name { get; set; }
        public string Id_Vendeur { get; set; }
        public int Quantite_stock { get; set; }
        public string image {  get; set; }
    }
}
