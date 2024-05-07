﻿// <auto-generated />
using BiblioMeta.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BiblioMeta.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BiblioMeta.Models.Avtor", b =>
                {
                    b.Property<int>("AvtorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AvtorID"));

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priimek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AvtorID");

                    b.ToTable("Avtor");
                });

            modelBuilder.Entity("BiblioMeta.Models.Knjiga", b =>
                {
                    b.Property<int>("KnjigaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KnjigaID"));

                    b.Property<int>("AvtorID")
                        .HasColumnType("int");

                    b.Property<float>("Cena")
                        .HasColumnType("real");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StStrani")
                        .HasColumnType("int");

                    b.Property<int>("StZnakov")
                        .HasColumnType("int");

                    b.Property<int>("ZanrID")
                        .HasColumnType("int");

                    b.HasKey("KnjigaID");

                    b.ToTable("Knjiga");
                });

            modelBuilder.Entity("BiblioMeta.Models.Zanr", b =>
                {
                    b.Property<int>("ZanrID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ZanrID"));

                    b.Property<string>("ImeZanra")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ZanrID");

                    b.ToTable("Zanr");
                });
#pragma warning restore 612, 618
        }
    }
}
