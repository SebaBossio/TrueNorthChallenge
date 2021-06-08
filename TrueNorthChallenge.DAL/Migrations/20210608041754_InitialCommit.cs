using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrueNorthChallenge.DAL.Migrations
{
    public partial class InitialCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Content = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    CTS = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CreatedById = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    MTS = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ModifiedById = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    CTS = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CreatedById = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    MTS = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ModifiedById = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts_Tags",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "INT", nullable: false),
                    TagId = table.Column<int>(type: "INT", nullable: false),
                    CTS = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CreatedById = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    MTS = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ModifiedById = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Tags_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "dbo",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Tags_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "dbo",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Tags_PostId",
                schema: "dbo",
                table: "Posts_Tags",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Tags_TagId",
                schema: "dbo",
                table: "Posts_Tags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts_Tags",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tag",
                schema: "dbo");
        }
    }
}
