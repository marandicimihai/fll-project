using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLL.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exhibit",
                columns: table => new
                {
                    ExhibitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExhibitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExhibitDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl2 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibit", x => x.ExhibitId);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExhibitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingValue = table.Column<double>(type: "float", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exhibit");

            migrationBuilder.DropTable(
                name: "Rating");
        }
    }
}
