﻿// <auto-generated />
using System;
using ChallengeApiPgSql.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChallengeApiPgSql.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220606175908_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChallengeApiPgSql.Models.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("ChallengeApiPgSql.Models.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Calificacion")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Estreno")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("GeneroId")
                        .HasColumnType("integer");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GeneroId");

                    b.ToTable("Peliculas");
                });

            modelBuilder.Entity("ChallengeApiPgSql.Models.Personaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Edad")
                        .HasColumnType("integer");

                    b.Property<string>("Historia")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Personajes");
                });

            modelBuilder.Entity("PeliculaPersonaje", b =>
                {
                    b.Property<int>("PeliculasId")
                        .HasColumnType("integer");

                    b.Property<int>("PersonajesId")
                        .HasColumnType("integer");

                    b.HasKey("PeliculasId", "PersonajesId");

                    b.HasIndex("PersonajesId");

                    b.ToTable("PeliculaPersonaje");
                });

            modelBuilder.Entity("ChallengeApiPgSql.Models.Pelicula", b =>
                {
                    b.HasOne("ChallengeApiPgSql.Models.Genero", null)
                        .WithMany("Peliculas")
                        .HasForeignKey("GeneroId");
                });

            modelBuilder.Entity("PeliculaPersonaje", b =>
                {
                    b.HasOne("ChallengeApiPgSql.Models.Pelicula", null)
                        .WithMany()
                        .HasForeignKey("PeliculasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChallengeApiPgSql.Models.Personaje", null)
                        .WithMany()
                        .HasForeignKey("PersonajesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ChallengeApiPgSql.Models.Genero", b =>
                {
                    b.Navigation("Peliculas");
                });
#pragma warning restore 612, 618
        }
    }
}