using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevProje.Migrations
{
    /// <inheritdoc />
    public partial class PoliklinikV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Yonetici",
                table: "Poliklinikler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Yonetici",
                table: "Poliklinikler",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
