﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MobileSnake.Database;

#nullable disable

namespace MobileSnake.Host.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231223181538_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("MobileSnake.Entities.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("creation_date_time");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER")
                        .HasColumnName("score");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("MobileSnake.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MobileSnake.Entities.Result", b =>
                {
                    b.HasOne("MobileSnake.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
