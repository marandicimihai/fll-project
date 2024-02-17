using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Rating",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Rating");
        }
    }
}
