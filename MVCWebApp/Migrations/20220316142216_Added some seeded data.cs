using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebApp.Migrations
{
    public partial class Addedsomeseededdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "ID", "City", "Name", "PhoneNumber" },
                values: new object[] { 1, "Stenstorp", "Sten Stensson", "0743345431" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "ID", "City", "Name", "PhoneNumber" },
                values: new object[] { 2, "Arboga", "Anna Aronsson", "0743345412" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "ID", "City", "Name", "PhoneNumber" },
                values: new object[] { 3, "Stockholm", "Jens Falk", "0743345444" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
