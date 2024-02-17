using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLL.Data.Migrations
{
    /// <inheritdoc />
    public partial class SmallChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Rating");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ExhibitId",
                table: "Rating",
                column: "ExhibitId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_UserId",
                table: "Rating",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Exhibit_ExhibitId",
                table: "Rating",
                column: "ExhibitId",
                principalTable: "Exhibit",
                principalColumn: "ExhibitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_User_UserId",
                table: "Rating",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Exhibit_ExhibitId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_User_UserId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_ExhibitId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_UserId",
                table: "Rating");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Rating",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
