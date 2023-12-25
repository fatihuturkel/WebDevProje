using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevProje.Migrations
{
    /// <inheritdoc />
    public partial class adminMi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "adminMi",
                table: "Kisiler",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "adminMi",
                table: "Kisiler");
        }
    }
}
