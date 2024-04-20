using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendre_pieces_auto.Migrations
{
    /// <inheritdoc />
    public partial class maigrationinitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Piece_Id_vendeur",
                table: "Piece",
                column: "Id_vendeur");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_Id_Acheteur",
                table: "Facture",
                column: "Id_Acheteur");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_Id_Commande",
                table: "Facture",
                column: "Id_Commande");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_Id_Vendeur",
                table: "Facture",
                column: "Id_Vendeur");

            migrationBuilder.AddForeignKey(
                name: "FK_Facture_Acheteur_Id_Acheteur",
                table: "Facture",
                column: "Id_Acheteur",
                principalTable: "Acheteur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facture_Commande_Id_Commande",
                table: "Facture",
                column: "Id_Commande",
                principalTable: "Commande",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facture_Vendeur_Id_Vendeur",
                table: "Facture",
                column: "Id_Vendeur",
                principalTable: "Vendeur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Piece_Vendeur_Id_vendeur",
                table: "Piece",
                column: "Id_vendeur",
                principalTable: "Vendeur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facture_Acheteur_Id_Acheteur",
                table: "Facture");

            migrationBuilder.DropForeignKey(
                name: "FK_Facture_Commande_Id_Commande",
                table: "Facture");

            migrationBuilder.DropForeignKey(
                name: "FK_Facture_Vendeur_Id_Vendeur",
                table: "Facture");

            migrationBuilder.DropForeignKey(
                name: "FK_Piece_Vendeur_Id_vendeur",
                table: "Piece");

            migrationBuilder.DropIndex(
                name: "IX_Piece_Id_vendeur",
                table: "Piece");

            migrationBuilder.DropIndex(
                name: "IX_Facture_Id_Acheteur",
                table: "Facture");

            migrationBuilder.DropIndex(
                name: "IX_Facture_Id_Commande",
                table: "Facture");

            migrationBuilder.DropIndex(
                name: "IX_Facture_Id_Vendeur",
                table: "Facture");
        }
    }
}
