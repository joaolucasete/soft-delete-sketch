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
                    { new Guid("b18400c7-68f7-45a7-ba0a-3eeeac9bb969"), null, false, "Person 3" },
                    { new Guid("b18400c7-68f7-49ca-9c4b-4b8dbea62eee"), null, false, "Person 2" },
                    { new Guid("b18400c7-68f7-4f0c-b667-f8850e942a48"), null, false, "Person 1" }
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "DeletionDateUtc", "IsDeleted", "Name", "OwnerId" },
                values: new object[,]
                {
                    { new Guid("b18400c7-68f7-4763-abc3-acff4006d0bb"), null, false, "Blog 5", new Guid("b18400c7-68f7-49ca-9c4b-4b8dbea62eee") },
                    { new Guid("b18400c7-68f7-47ee-b7df-af900a550471"), null, false, "Blog 3", new Guid("b18400c7-68f7-45a7-ba0a-3eeeac9bb969") },
                    { new Guid("b18400c7-68f7-4885-84a2-8c619355244c"), null, false, "Blog 1", new Guid("b18400c7-68f7-4f0c-b667-f8850e942a48") },
                    { new Guid("b18400c7-68f7-49f8-a59e-1c43b8c22e77"), null, false, "Blog 4", new Guid("b18400c7-68f7-4f0c-b667-f8850e942a48") },
                    { new Guid("b18400c7-68f7-4a9b-a636-5a069471fd56"), null, false, "Blog 6", new Guid("b18400c7-68f7-45a7-ba0a-3eeeac9bb969") },
                    { new Guid("b18400c7-68f7-4fdf-9b4d-c4409a1724d9"), null, false, "Blog 2", new Guid("b18400c7-68f7-49ca-9c4b-4b8dbea62eee") }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "BlogId", "Content", "DeletionDateUtc", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { new Guid("b18400c7-68f7-4051-956a-9ad3ad25fc11"), new Guid("b18400c7-68f7-4f0c-b667-f8850e942a48"), new Guid("b18400c7-68f7-4885-84a2-8c619355244c"), "Content 1", null, false, "Post 1" },
                    { new Guid("b18400c7-68f7-4061-a5bf-5706d1b064a9"), new Guid("b18400c7-68f7-45a7-ba0a-3eeeac9bb969"), new Guid("b18400c7-68f7-47ee-b7df-af900a550471"), "Content 3", null, false, "Post 3" },
                    { new Guid("b18400c7-68f7-46a7-ab22-949807d4abdf"), new Guid("b18400c7-68f7-45a7-ba0a-3eeeac9bb969"), new Guid("b18400c7-68f7-4a9b-a636-5a069471fd56"), "Content 2", null, false, "Post 6" },
                    { new Guid("b18400c7-68f7-4713-8267-1f404f0b1a88"), new Guid("b18400c7-68f7-49ca-9c4b-4b8dbea62eee"), new Guid("b18400c7-68f7-4fdf-9b4d-c4409a1724d9"), "Content 2", null, false, "Post 2" },
                    { new Guid("b18400c7-68f7-4866-ae8d-75d223e75a43"), new Guid("b18400c7-68f7-49ca-9c4b-4b8dbea62eee"), new Guid("b18400c7-68f7-4763-abc3-acff4006d0bb"), "Content 2", null, false, "Post 5" },
                    { new Guid("b18400c7-68f7-4ed2-883d-52be39aa5d10"), new Guid("b18400c7-68f7-4f0c-b667-f8850e942a48"), new Guid("b18400c7-68f7-49f8-a59e-1c43b8c22e77"), "Content 1", null, false, "Post 4" }
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
