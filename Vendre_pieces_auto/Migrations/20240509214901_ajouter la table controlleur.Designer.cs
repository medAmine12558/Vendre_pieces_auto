﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vendre_pieces_auto.Data;

#nullable disable

namespace Vendre_pieces_auto.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240509214901_ajouter la table controlleur")]
    partial class ajouterlatablecontrolleur
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Commande", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Id_Acheteur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_Piece")
                        .HasColumnType("int");

                    b.Property<string>("Id_Vendeur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Qantite")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Commande");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Controlleur", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Controleur");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Facture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AcheteurId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_Commande")
                        .HasColumnType("int");

                    b.Property<string>("VendeurId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id_Commande");

                    b.ToTable("Facture");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Piece", b =>
                {
                    b.Property<int>("Id_piece")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_piece"));

                    b.Property<string>("Id_Vendeur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom_piece")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantite_stock")
                        .HasColumnType("int");

                    b.Property<string>("Type_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_piece");

                    b.ToTable("Piece");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Facture", b =>
                {
                    b.HasOne("Vendre_pieces_auto.Models.Tabels.Commande", "Commande")
                        .WithMany()
                        .HasForeignKey("Id_Commande")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Commande");
                });
#pragma warning restore 612, 618
        }
    }
}
