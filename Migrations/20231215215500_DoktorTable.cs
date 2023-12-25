using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevProje.Migrations
{
    /// <inheritdoc />
    public partial class DoktorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doktorlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Maas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PoliklinikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doktorlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doktorlar_Kisiler_Id",
                        column: x => x.Id,
                        principalTable: "Kisiler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doktorlar_Poliklinikler_PoliklinikId",
                        column: x => x.PoliklinikId,
                        principalTable: "Poliklinikler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doktorlar_PoliklinikId",
                table: "Doktorlar",
                column: "PoliklinikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doktorlar");
        }
    }
}
