using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendre_pieces_auto.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    Id_Acheteur = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commande", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Controleur",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controleur", x => x.Id);
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
                    is_valide = table.Column<bool>(type: "bit", nullable: false),
                    prix = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piece", x => x.Id_piece);
                });

            migrationBuilder.CreateTable(
                name: "Facture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Comm = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facture_Commande_Id_Comm",
                        column: x => x.Id_Comm,
                        principalTable: "Commande",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandePiece",
                columns: table => new
                {
                    Commande_id = table.Column<int>(type: "int", nullable: false),
                    Piece_id = table.Column<int>(type: "int", nullable: false),
                    quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandePiece", x => new { x.Piece_id, x.Commande_id });
                    table.ForeignKey(
                        name: "FK_CommandePiece_Commande_Commande_id",
                        column: x => x.Commande_id,
                        principalTable: "Commande",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandePiece_Piece_Piece_id",
                        column: x => x.Piece_id,
                        principalTable: "Piece",
                        principalColumn: "Id_piece",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favoris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_client = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_piece = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favoris_Piece_id_piece",
                        column: x => x.id_piece,
                        principalTable: "Piece",
                        principalColumn: "Id_piece",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id_image = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    id_Piece = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id_image);
                    table.ForeignKey(
                        name: "FK_Photos_Piece_id_Piece",
                        column: x => x.id_Piece,
                        principalTable: "Piece",
                        principalColumn: "Id_piece",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandePiece_Commande_id",
                table: "CommandePiece",
                column: "Commande_id");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_Id_Comm",
                table: "Facture",
                column: "Id_Comm");

            migrationBuilder.CreateIndex(
                name: "IX_Favoris_id_piece",
                table: "Favoris",
                column: "id_piece");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_id_Piece",
                table: "Photos",
                column: "id_Piece");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandePiece");

            migrationBuilder.DropTable(
                name: "Controleur");

            migrationBuilder.DropTable(
                name: "Facture");

            migrationBuilder.DropTable(
                name: "Favoris");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Commande");

            migrationBuilder.DropTable(
                name: "Piece");
        }
    }
}
