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
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletionDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    { new Guid("b1810132-67f1-44c6-b214-3fe7e2baacb8"), null, false, "Person 2" },
                    { new Guid("b1810132-67f1-45d2-a02a-3747213080a2"), null, false, "Person 3" },
                    { new Guid("b1810132-67f1-4d6d-809e-a17e581d52fc"), null, false, "Person 1" }
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "DeletionDateUtc", "IsDeleted", "Name", "OwnerId" },
                values: new object[,]
                {
                    { new Guid("b1810132-67f1-441d-92cc-dc38f30a52c1"), null, false, "Blog 3", new Guid("b1810132-67f1-45d2-a02a-3747213080a2") },
                    { new Guid("b1810132-67f1-4466-ad34-01c309274adc"), null, false, "Blog 6", new Guid("b1810132-67f1-45d2-a02a-3747213080a2") },
                    { new Guid("b1810132-67f1-4824-aa0f-62c53f1ed891"), null, false, "Blog 2", new Guid("b1810132-67f1-44c6-b214-3fe7e2baacb8") },
                    { new Guid("b1810132-67f1-4b14-bb91-d877349f1bd1"), null, false, "Blog 1", new Guid("b1810132-67f1-4d6d-809e-a17e581d52fc") },
                    { new Guid("b1810132-67f1-4dd1-9dfb-a43dbb7e529c"), null, false, "Blog 4", new Guid("b1810132-67f1-4d6d-809e-a17e581d52fc") },
                    { new Guid("b1810132-67f1-4f70-bf11-76aa5505ea76"), null, false, "Blog 5", new Guid("b1810132-67f1-44c6-b214-3fe7e2baacb8") }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "BlogId", "Content", "DeletionDateUtc", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { new Guid("b1810132-67f1-41a6-b7c3-38ce409f308a"), new Guid("b1810132-67f1-44c6-b214-3fe7e2baacb8"), new Guid("b1810132-67f1-4dd1-9dfb-a43dbb7e529c"), "Content 2", null, false, "Post 5" },
                    { new Guid("b1810132-67f1-42fe-9918-67320f46c1eb"), new Guid("b1810132-67f1-44c6-b214-3fe7e2baacb8"), new Guid("b1810132-67f1-4dd1-9dfb-a43dbb7e529c"), "Content 2", null, false, "Post 2" },
                    { new Guid("b1810132-67f1-4e4a-bf26-fa20f9e68629"), new Guid("b1810132-67f1-4d6d-809e-a17e581d52fc"), new Guid("b1810132-67f1-4b14-bb91-d877349f1bd1"), "Content 1", null, false, "Post 4" },
                    { new Guid("b1810132-67f1-4ecb-b27d-2b545578b274"), new Guid("b1810132-67f1-45d2-a02a-3747213080a2"), new Guid("b1810132-67f1-4824-aa0f-62c53f1ed891"), "Content 3", null, false, "Post 3" },
                    { new Guid("b1810132-67f1-4ee0-a6cf-884d33a84dc1"), new Guid("b1810132-67f1-4d6d-809e-a17e581d52fc"), new Guid("b1810132-67f1-4b14-bb91-d877349f1bd1"), "Content 1", null, false, "Post 1" },
                    { new Guid("b1810132-67f1-4f3f-be39-4088c4b4a9a8"), new Guid("b1810132-67f1-45d2-a02a-3747213080a2"), new Guid("b1810132-67f1-4824-aa0f-62c53f1ed891"), "Content 2", null, false, "Post 6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_IsDeleted",
                table: "Blogs",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_OwnerId",
                table: "Blogs",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_People_IsDeleted",
                table: "People",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_IsDeleted",
                table: "Posts",
                column: "IsDeleted");
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
