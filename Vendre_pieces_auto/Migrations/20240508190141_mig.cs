using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendre_pieces_auto.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commande",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Acheteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Vendeur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Piece = table.Column<int>(type: "int", nullable: false),
                    Qantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commande", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "controleur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_controleur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Piece",
                columns: table => new
                {
                    Id_piece = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom_piece = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Vendeur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantite_stock = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piece", x => x.Id_piece);
                });

            migrationBuilder.CreateTable(
                name: "Facture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendeurId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcheteurId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Commande = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facture_Commande_Id_Commande",
                        column: x => x.Id_Commande,
                        principalTable: "Commande",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facture_Id_Commande",
                table: "Facture",
                column: "Id_Commande");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "controleur");

            migrationBuilder.DropTable(
                name: "Facture");

            migrationBuilder.DropTable(
                name: "Piece");

            migrationBuilder.DropTable(
                name: "Commande");
        }
    }
}
