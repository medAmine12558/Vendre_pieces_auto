﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vendre_pieces_auto.Data;

#nullable disable

namespace Vendre_pieces_auto.Migrations
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

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Commande", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Id_Acheteur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id_Vendeur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Commande");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Commander", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Commande_id")
                        .HasColumnType("int");

                    b.Property<int?>("PieceId_piece")
                        .HasColumnType("int");

                    b.Property<string>("Piece_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("quantite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Commande_id");

                    b.HasIndex("PieceId_piece");

                    b.ToTable("Commander");
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Id_Acheteur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_Comm")
                        .HasColumnType("int");

                    b.Property<string>("Id_Vendeur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id_Comm");

                    b.ToTable("Facture");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Favoris", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("id_piece")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("id_piece");

                    b.ToTable("Favoris");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Photos", b =>
                {
                    b.Property<string>("Id_image")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("id_Piece")
                        .HasColumnType("int");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_image");

                    b.HasIndex("id_Piece");

                    b.ToTable("Photos");
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

                    b.Property<bool>("is_valide")
                        .HasColumnType("bit");

                    b.Property<float>("prix")
                        .HasColumnType("real");

                    b.HasKey("Id_piece");

                    b.ToTable("Piece");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Commander", b =>
                {
                    b.HasOne("Vendre_pieces_auto.Models.Tabels.Commande", "Commande")
                        .WithMany("Commanders")
                        .HasForeignKey("Commande_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vendre_pieces_auto.Models.Tabels.Piece", null)
                        .WithMany("Commanders")
                        .HasForeignKey("PieceId_piece");

                    b.Navigation("Commande");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Facture", b =>
                {
                    b.HasOne("Vendre_pieces_auto.Models.Tabels.Commande", "Commande")
                        .WithMany()
                        .HasForeignKey("Id_Comm")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Commande");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Favoris", b =>
                {
                    b.HasOne("Vendre_pieces_auto.Models.Tabels.Piece", "piece")
                        .WithMany()
                        .HasForeignKey("id_piece")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("piece");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Photos", b =>
                {
                    b.HasOne("Vendre_pieces_auto.Models.Tabels.Piece", "Piece")
                        .WithMany("Photos")
                        .HasForeignKey("id_Piece")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Piece");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Commande", b =>
                {
                    b.Navigation("Commanders");
                });

            modelBuilder.Entity("Vendre_pieces_auto.Models.Tabels.Piece", b =>
                {
                    b.Navigation("Commanders");

                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
