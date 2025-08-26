using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaTimeProj.Migrations
{
    /// <inheritdoc />
    public partial class Modify_CategoryItemName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayOrrder",
                table: "Categories",
                newName: "DisplayOrder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayOrder",
                table: "Categories",
                newName: "DisplayOrrder");
        }
    }
}
