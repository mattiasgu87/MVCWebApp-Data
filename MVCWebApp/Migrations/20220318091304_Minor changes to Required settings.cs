using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebApp.Migrations
{
    public partial class MinorchangestoRequiredsettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryForeignKey",
                table: "Cities");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryForeignKey",
                table: "Cities",
                column: "CountryForeignKey",
                principalTable: "Countries",
                principalColumn: "CountryName",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryForeignKey",
                table: "Cities");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryForeignKey",
                table: "Cities",
                column: "CountryForeignKey",
                principalTable: "Countries",
                principalColumn: "CountryName",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
