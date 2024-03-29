﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sqlite;

#nullable disable

namespace Sqlite.Migrations
{
    [DbContext(typeof(SqliteContext))]
    partial class SqliteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("Domain.Assignation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MemberId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Assignation");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MemberId = 1,
                            ProjectId = 1,
                            Role = "Team Lead"
                        },
                        new
                        {
                            Id = 2,
                            MemberId = 1,
                            ProjectId = 2,
                            Role = "Advisor"
                        },
                        new
                        {
                            Id = 3,
                            MemberId = 2,
                            ProjectId = 2,
                            Role = "Developer"
                        });
                });

            modelBuilder.Entity("Domain.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "john@doe.com",
                            FullName = "John Doe"
                        },
                        new
                        {
                            Id = 2,
                            Email = "simon@piece.com",
                            FullName = "Simon Piece"
                        });
                });

            modelBuilder.Entity("Domain.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "R&D"
                        },
                        new
                        {
                            Id = 2,
                            Name = "SDK"
                        });
                });

            modelBuilder.Entity("Domain.Assignation", b =>
                {
                    b.HasOne("Domain.Member", "Member")
                        .WithMany("Assignations")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Project", "Project")
                        .WithMany("Assignation")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Domain.Member", b =>
                {
                    b.Navigation("Assignations");
                });

            modelBuilder.Entity("Domain.Project", b =>
                {
                    b.Navigation("Assignation");
                });
#pragma warning restore 612, 618
        }
    }
}
