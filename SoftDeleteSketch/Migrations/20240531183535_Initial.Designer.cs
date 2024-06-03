﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SoftDeleteSketch.Entities;

#nullable disable

namespace SoftDeleteSketch.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240531183535_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SoftDeleteSketch.Entities.Blog", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletionDateUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("OwnerId");

                    b.ToTable("Blogs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b1810132-67f1-4b14-bb91-d877349f1bd1"),
                            IsDeleted = false,
                            Name = "Blog 1",
                            OwnerId = new Guid("b1810132-67f1-4d6d-809e-a17e581d52fc")
                        },
                        new
                        {
                            Id = new Guid("b1810132-67f1-4dd1-9dfb-a43dbb7e529c"),
                            IsDeleted = false,
                            Name = "Blog 4",
                            OwnerId = new Guid("b1810132-67f1-4d6d-809e-a17e581d52fc")
                        },
                        new
                        {
                            Id = new Guid("b1810132-67f1-4824-aa0f-62c53f1ed891"),
                            IsDeleted = false,
                            Name = "Blog 2",
                            OwnerId = new Guid("b1810132-67f1-44c6-b214-3fe7e2baacb8")
                        },
                        new
                        {
                            Id = new Guid("b1810132-67f1-4f70-bf11-76aa5505ea76"),
                            IsDeleted = false,
                            Name = "Blog 5",
                            OwnerId = new Guid("b1810132-67f1-44c6-b214-3fe7e2baacb8")
                        },
                        new
                        {
                            Id = new Guid("b1810132-67f1-441d-92cc-dc38f30a52c1"),
                            IsDeleted = false,
                            Name = "Blog 3",
                            OwnerId = new Guid("b1810132-67f1-45d2-a02a-3747213080a2")
                        },
                        new
                        {
                            Id = new Guid("b1810132-67f1-4466-ad34-01c309274adc"),
                            IsDeleted = false,
                            Name = "Blog 6",
                            OwnerId = new Guid("b1810132-67f1-45d2-a02a-3747213080a2")
                        });
                });

            modelBuilder.Entity("SoftDeleteSketch.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletionDateUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b1810132-67f1-4d6d-809e-a17e581d52fc"),
                            IsDeleted = false,
                            Name = "Person 1"
                        },
                        new
                        {
                            Id = new Guid("b1810132-67f1-44c6-b214-3fe7e2baacb8"),
                            IsDeleted = false,
                            Name = "Person 2"
                        },
                        new
                        {
                            Id = new Guid("b1810132-67f1-45d2-a02a-3747213080a2"),
                            IsDeleted = false,
                            Name = "Person 3"
                        });
                });

            modelBuilder.Entity("SoftDeleteSketch.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BlogId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletionDateUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BlogId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b1810132-67f1-4ee0-a6cf-884d33a84dc1"),
                            AuthorId = new Guid("b1810132-67f1-4d6d-809e-a17e581d52fc"),
                            BlogId = new Guid("b1810132-67f1-4b14-bb91-d877349f1bd1"),
                            Content = "Content 1",
                            IsDeleted = false,
                            Title = "Post 1"
                        },
                        new
                        {
                            Id = new Guid("b1810132-67f1-4e4a-bf26-fa20f9e68629"),
                            AuthorId = new Guid("b1810132-67f1-4d6d-809e-a17e581d52fc"),
                            BlogId = new Guid("b1810132-67f1-4b14-bb91-d877349f1bd1"),
                            Content = "Content 1",
                            IsDeleted = false,
                            Title = "Post 4"
                        },
                        new
                        {
                            Id = new Guid("b1810132-67f1-42fe-9918-67320f46c1eb"),
                            AuthorId = new Guid("b1810132-67f1-44c6-b214-3fe7e2baacb8"),
                            BlogId = new Guid("b1810132-67f1-4dd1-9dfb-a43dbb7e529c"),
                            Content = "Content 2",
                            IsDeleted = false,
                            Title = "Post 2"
                        },
                        new
                        {
                            Id = new Guid("b1810132-67f1-41a6-b7c3-38ce409f308a"),
                            AuthorId = new Guid("b1810132-67f1-44c6-b214-3fe7e2baacb8"),
                            BlogId = new Guid("b1810132-67f1-4dd1-9dfb-a43dbb7e529c"),
                            Content = "Content 2",
                            IsDeleted = false,
                            Title = "Post 5"
                        },
                        new
                        {
                            Id = new Guid("b1810132-67f1-4ecb-b27d-2b545578b274"),
                            AuthorId = new Guid("b1810132-67f1-45d2-a02a-3747213080a2"),
                            BlogId = new Guid("b1810132-67f1-4824-aa0f-62c53f1ed891"),
                            Content = "Content 3",
                            IsDeleted = false,
                            Title = "Post 3"
                        },
                        new
                        {
                            Id = new Guid("b1810132-67f1-4f3f-be39-4088c4b4a9a8"),
                            AuthorId = new Guid("b1810132-67f1-45d2-a02a-3747213080a2"),
                            BlogId = new Guid("b1810132-67f1-4824-aa0f-62c53f1ed891"),
                            Content = "Content 2",
                            IsDeleted = false,
                            Title = "Post 6"
                        });
                });

            modelBuilder.Entity("SoftDeleteSketch.Entities.Blog", b =>
                {
                    b.HasOne("SoftDeleteSketch.Entities.Person", "Owner")
                        .WithMany("Blogs")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SoftDeleteSketch.Entities.Post", b =>
                {
                    b.HasOne("SoftDeleteSketch.Entities.Person", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftDeleteSketch.Entities.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("SoftDeleteSketch.Entities.Blog", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("SoftDeleteSketch.Entities.Person", b =>
                {
                    b.Navigation("Blogs");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}