using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendre_pieces_auto.Migrations
{
    /// <inheritdoc />
    public partial class cccc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Piece");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Piece",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
