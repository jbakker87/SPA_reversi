﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SPA.DAL;

namespace SPA.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20190322164247_arrayToDB")]
    partial class arrayToDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SPA.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("GameToken");

                    b.Property<int>("PlayerBlackId");

                    b.Property<int>("PlayerWhiteId");

                    b.Property<int>("Turn");

                    b.Property<string>("boardString");

                    b.HasKey("GameId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("SPA.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GameId");

                    b.Property<string>("PlayerConfirmedPassword")
                        .IsRequired();

                    b.Property<string>("PlayerEmail")
                        .IsRequired();

                    b.Property<string>("PlayerName")
                        .IsRequired();

                    b.Property<string>("PlayerPassword")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PlayerProviderKey");

                    b.Property<string>("PlayerRole");

                    b.Property<string>("PlayerToken");

                    b.HasKey("PlayerId");

                    b.HasIndex("GameId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("SPA.Models.Player", b =>
                {
                    b.HasOne("SPA.Models.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId");
                });
#pragma warning restore 612, 618
        }
    }
}
