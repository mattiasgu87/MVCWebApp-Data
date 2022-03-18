using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebApp.Migrations
{
    public partial class AddedseededdatatoLanguageandPersonLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Languages",
                column: "LanguageName",
                value: "Franska");

            migrationBuilder.InsertData(
                table: "Languages",
                column: "LanguageName",
                value: "Polska");

            migrationBuilder.InsertData(
                table: "Languages",
                column: "LanguageName",
                value: "Italienska");

            migrationBuilder.InsertData(
                table: "PersonLanguages",
                columns: new[] { "PersonId", "LanguageName" },
                values: new object[] { 1, "Franska" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageName",
                keyValue: "Italienska");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageName",
                keyValue: "Polska");

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageName" },
                keyValues: new object[] { 1, "Franska" });

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageName",
                keyValue: "Franska");
        }
    }
}
