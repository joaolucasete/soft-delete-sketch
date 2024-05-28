using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftDeleteSketch.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletionDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletionDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_People_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    BlogId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_People_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "DeletionDateUtc", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("b17800bf-9952-41ab-b8bd-1e071945bb1a"), null, false, "Person 2" },
                    { new Guid("b17800bf-9952-47b7-9fe2-7a7b0f6b2de0"), null, false, "Person 3" },
                    { new Guid("b17800bf-9952-4ae3-b1d5-585948a67252"), null, false, "Person 1" }
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "DeletionDateUtc", "IsDeleted", "Name", "OwnerId" },
                values: new object[,]
                {
                    { new Guid("b17800bf-9952-403e-b1df-c93be3dc9c03"), null, false, "Blog 3", new Guid("b17800bf-9952-4ae3-b1d5-585948a67252") },
                    { new Guid("b17800bf-9952-4d51-a4fc-96c2a84b466e"), null, false, "Blog 2", new Guid("b17800bf-9952-41ab-b8bd-1e071945bb1a") },
                    { new Guid("b17800bf-9952-4e0b-b793-bb629162b009"), null, false, "Blog 1", new Guid("b17800bf-9952-47b7-9fe2-7a7b0f6b2de0") }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "BlogId", "Content", "Title" },
                values: new object[,]
                {
                    { new Guid("b17800bf-9952-4491-894e-720664bff71c"), new Guid("b17800bf-9952-4ae3-b1d5-585948a67252"), new Guid("b17800bf-9952-4e0b-b793-bb629162b009"), "Content 3", "Post 3" },
                    { new Guid("b17800bf-9952-469e-958b-dbcf673992c3"), new Guid("b17800bf-9952-47b7-9fe2-7a7b0f6b2de0"), new Guid("b17800bf-9952-403e-b1df-c93be3dc9c03"), "Content 1", "Post 1" },
                    { new Guid("b17800bf-9952-4dbe-9325-37e53fc147b0"), new Guid("b17800bf-9952-41ab-b8bd-1e071945bb1a"), new Guid("b17800bf-9952-4d51-a4fc-96c2a84b466e"), "Content 2", "Post 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_OwnerId",
                table: "Blogs",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
