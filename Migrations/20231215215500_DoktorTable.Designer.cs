﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebDevProje.Models;

#nullable disable

namespace WebDevProje.Migrations
{
    [DbContext(typeof(HastaneContext))]
    [Migration("20231215215500_DoktorTable")]
    partial class DoktorTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebDevProje.Models.AnabilimDali", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Aciklama")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("AktiflikDurumu")
                        .HasColumnType("bit");

                    b.Property<string>("Eposta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FaxNo")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("KurulusTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("TelefonNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("YoneticiId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("YoneticiId");

                    b.ToTable("AnabilimDallari");
                });

            modelBuilder.Entity("WebDevProje.Models.Doktor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Maas")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PoliklinikId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PoliklinikId");

                    b.ToTable("Doktorlar");
                });

            modelBuilder.Entity("WebDevProje.Models.Kisi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Cinsiyet")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<DateTime>("DogumTarihi")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Doktor")
                        .HasColumnType("bit");

                    b.Property<string>("Eposta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Hasta")
                        .HasColumnType("bit");

                    b.Property<bool>("Hemsire")
                        .HasColumnType("bit");

                    b.Property<bool>("Isci")
                        .HasColumnType("bit");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TcKimlikNo")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("TelefonNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("Yonetici")
                        .HasColumnType("bit");

                    b.Property<bool>("adminMi")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Kisiler");
                });

            modelBuilder.Entity("WebDevProje.Models.Poliklinik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Aciklama")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("AktiflikDurumu")
                        .HasColumnType("bit");

                    b.Property<int>("AnabilimDaliId")
                        .HasColumnType("int");

                    b.Property<string>("Eposta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FaxNo")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("KurulusTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("TelefonNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("AnabilimDaliId");

                    b.ToTable("Poliklinikler");
                });

            modelBuilder.Entity("WebDevProje.Models.AnabilimDali", b =>
                {
                    b.HasOne("WebDevProje.Models.Kisi", "Yonetici")
                        .WithMany()
                        .HasForeignKey("YoneticiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Yonetici");
                });

            modelBuilder.Entity("WebDevProje.Models.Doktor", b =>
                {
                    b.HasOne("WebDevProje.Models.Kisi", "Kisi")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebDevProje.Models.Poliklinik", "Poliklinik")
                        .WithMany()
                        .HasForeignKey("PoliklinikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kisi");

                    b.Navigation("Poliklinik");
                });

            modelBuilder.Entity("WebDevProje.Models.Poliklinik", b =>
                {
                    b.HasOne("WebDevProje.Models.AnabilimDali", "AnabilimDali")
                        .WithMany()
                        .HasForeignKey("AnabilimDaliId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnabilimDali");
                });
#pragma warning restore 612, 618
        }
    }
}
