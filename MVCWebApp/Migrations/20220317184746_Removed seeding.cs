using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebApp.Migrations
{
    public partial class Removedseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Cities_CityID",
                table: "People");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryName",
                keyValue: "Damnark");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryName",
                keyValue: "Norge");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryName",
                keyValue: "Sverige");

            migrationBuilder.AlterColumn<int>(
                name: "CityID",
                table: "People",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Cities_CityID",
                table: "People",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Cities_CityID",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "CityID",
                table: "People",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Countries",
                column: "CountryName",
                value: "Sverige");

            migrationBuilder.InsertData(
                table: "Countries",
                column: "CountryName",
                value: "Norge");

            migrationBuilder.InsertData(
                table: "Countries",
                column: "CountryName",
                value: "Damnark");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Cities_CityID",
                table: "People",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
