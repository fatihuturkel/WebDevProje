using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevProje.Migrations
{
    /// <inheritdoc />
    public partial class doktorCalismaTakvimi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saat",
                table: "Randevular");

            migrationBuilder.CreateTable(
                name: "DoktorCalismaTakvimleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoktorId = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dokuz_on = table.Column<int>(type: "int", nullable: false),
                    on_onbir = table.Column<int>(type: "int", nullable: false),
                    onbir_oniki = table.Column<int>(type: "int", nullable: false),
                    onuc_ondort = table.Column<int>(type: "int", nullable: false),
                    ondort_onbes = table.Column<int>(type: "int", nullable: false),
                    onbes_onalti = table.Column<int>(type: "int", nullable: false),
                    onalti_onyedi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoktorCalismaTakvimleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoktorCalismaTakvimleri_Doktorlar_DoktorId",
                        column: x => x.DoktorId,
                        principalTable: "Doktorlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoktorCalismaTakvimleri_DoktorId",
                table: "DoktorCalismaTakvimleri",
                column: "DoktorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoktorCalismaTakvimleri");

            migrationBuilder.AddColumn<string>(
                name: "Saat",
                table: "Randevular",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
