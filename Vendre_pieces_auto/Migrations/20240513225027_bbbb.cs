using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendre_pieces_auto.Migrations
{
    /// <inheritdoc />
    public partial class bbbb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_Photos_id_Piece",
                table: "Photos",
                column: "id_Piece");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");
        }
    }
}
