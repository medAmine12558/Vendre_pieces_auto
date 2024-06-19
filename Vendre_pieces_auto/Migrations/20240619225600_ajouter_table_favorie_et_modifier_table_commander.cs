using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendre_pieces_auto.Migrations
{
    /// <inheritdoc />
    public partial class ajouter_table_favorie_et_modifier_table_commander : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commander_Piece_Piece_id",
                table: "Commander");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Commander_Piece_id",
                table: "Commander",
                column: "Piece_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Commander_Piece_Piece_id",
                table: "Commander",
                column: "Piece_id",
                principalTable: "Piece",
                principalColumn: "Id_piece",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
