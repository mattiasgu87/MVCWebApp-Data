using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebApp.Migrations
{
    public partial class Newseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryName",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Cities_CityID",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_CityID",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryName",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CityID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Cities");

            migrationBuilder.AddColumn<int>(
                name: "CityForeignKey",
                table: "People",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CountryForeignKey",
                table: "Cities",
                nullable: true);

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
                value: "Danmark");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "CityName", "CountryForeignKey" },
                values: new object[] { 1, "Stockholm", "Sverige" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "CityName", "CountryForeignKey" },
                values: new object[] { 2, "Oslo", "Norge" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "CityName", "CountryForeignKey" },
                values: new object[] { 3, "Köpenhamn", "Danmark" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "ID", "CityForeignKey", "Name", "PhoneNumber" },
                values: new object[] { 1, 1, "Jens Jensson", "123456789" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "ID", "CityForeignKey", "Name", "PhoneNumber" },
                values: new object[] { 2, 2, "Anna Andersson", "987654321" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "ID", "CityForeignKey", "Name", "PhoneNumber" },
                values: new object[] { 3, 3, "Sven Svensson", "123123123" });

            migrationBuilder.CreateIndex(
                name: "IX_People_CityForeignKey",
                table: "People",
                column: "CityForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryForeignKey",
                table: "Cities",
                column: "CountryForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryForeignKey",
                table: "Cities",
                column: "CountryForeignKey",
                principalTable: "Countries",
                principalColumn: "CountryName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Cities_CityForeignKey",
                table: "People",
                column: "CityForeignKey",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryForeignKey",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Cities_CityForeignKey",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_CityForeignKey",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryForeignKey",
                table: "Cities");

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

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryName",
                keyValue: "Danmark");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryName",
                keyValue: "Norge");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryName",
                keyValue: "Sverige");

            migrationBuilder.DropColumn(
                name: "CityForeignKey",
                table: "People");

            migrationBuilder.DropColumn(
                name: "CountryForeignKey",
                table: "Cities");

            migrationBuilder.AddColumn<int>(
                name: "CityID",
                table: "People",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Cities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_CityID",
                table: "People",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryName",
                table: "Cities",
                column: "CountryName");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryName",
                table: "Cities",
                column: "CountryName",
                principalTable: "Countries",
                principalColumn: "CountryName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Cities_CityID",
                table: "People",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
