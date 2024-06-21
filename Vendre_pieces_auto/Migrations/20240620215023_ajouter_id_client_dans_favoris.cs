using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendre_pieces_auto.Migrations
{
    /// <inheritdoc />
    public partial class ajouter_id_client_dans_favoris : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id_client",
                table: "Favoris",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_client",
                table: "Favoris");
        }
    }
}
