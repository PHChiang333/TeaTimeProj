using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeaTimeProj.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Address", "City", "Description", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "台中市北區三民路三段129號", "台中市", "台中一中學生最愛", "台中一中店", "04-12345678" },
                    { 2, "台北市中正區羅斯福路四段85號", "台北市", "台大、師大學生最愛", "台北公館店", "02-12345678" },
                    { 3, "高雄市左營區博愛二路777號", "高雄市", "高雄巨蛋附近最愛", "高雄巨蛋店", "07-12345678" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
