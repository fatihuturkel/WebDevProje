using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevProje.Migrations
{
    /// <inheritdoc />
    public partial class Ilk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kisiler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cinsiyet = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TelefonNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Doktor = table.Column<bool>(type: "bit", nullable: false),
                    Hasta = table.Column<bool>(type: "bit", nullable: false),
                    Hemsire = table.Column<bool>(type: "bit", nullable: false),
                    Isci = table.Column<bool>(type: "bit", nullable: false),
                    Yonetici = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kisiler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnabilimDallari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YoneticiId = table.Column<int>(type: "int", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TelefonNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FaxNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Eposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KurulusTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AktiflikDurumu = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnabilimDallari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnabilimDallari_Kisiler_YoneticiId",
                        column: x => x.YoneticiId,
                        principalTable: "Kisiler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poliklinikler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Yonetici = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TelefonNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FaxNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Eposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KurulusTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AktiflikDurumu = table.Column<bool>(type: "bit", nullable: false),
                    AnabilimDaliId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliklinikler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poliklinikler_AnabilimDallari_AnabilimDaliId",
                        column: x => x.AnabilimDaliId,
                        principalTable: "AnabilimDallari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnabilimDallari_YoneticiId",
                table: "AnabilimDallari",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Poliklinikler_AnabilimDaliId",
                table: "Poliklinikler",
                column: "AnabilimDaliId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Poliklinikler");

            migrationBuilder.DropTable(
                name: "AnabilimDallari");

            migrationBuilder.DropTable(
                name: "Kisiler");
        }
    }
}
