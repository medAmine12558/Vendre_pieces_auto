using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendre_pieces_auto.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commander_Piece_PieceId_piece",
                table: "Commander");

            migrationBuilder.DropIndex(
                name: "IX_Commander_PieceId_piece",
                table: "Commander");

            migrationBuilder.DropColumn(
                name: "PieceId_piece",
                table: "Commander");

            migrationBuilder.AlterColumn<int>(
                name: "Piece_id",
                table: "Commander",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Favoris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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

            migrationBuilder.CreateIndex(
                name: "IX_Commander_Piece_id",
                table: "Commander",
                column: "Piece_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favoris_id_piece",
                table: "Favoris",
                column: "id_piece");

            migrationBuilder.AddForeignKey(
                name: "FK_Commander_Piece_Piece_id",
                table: "Commander",
                column: "Piece_id",
                principalTable: "Piece",
                principalColumn: "Id_piece",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commander_Piece_Piece_id",
                table: "Commander");

            migrationBuilder.DropTable(
                name: "Favoris");

            migrationBuilder.DropIndex(
                name: "IX_Commander_Piece_id",
                table: "Commander");

            migrationBuilder.AlterColumn<string>(
                name: "Piece_id",
                table: "Commander",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PieceId_piece",
                table: "Commander",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commander_PieceId_piece",
                table: "Commander",
                column: "PieceId_piece");

            migrationBuilder.AddForeignKey(
                name: "FK_Commander_Piece_PieceId_piece",
                table: "Commander",
                column: "PieceId_piece",
                principalTable: "Piece",
                principalColumn: "Id_piece");
        }
    }
}
