using Microsoft.EntityFrameworkCore.Migrations;

namespace asp_mvc.Migrations
{
    public partial class movieencryption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "QmLIoDCR4+XdfbABuDKlxGnRf8rgJSxuFKlaxXV5p+c=");

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "QqcNP7BNJ4p4G056h4ZXGw==");

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "8dCMc07jZ4lGIgsOTsCnRA==");

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 4,
                column: "Title",
                value: "TUfObcZkBTNczp+QeDa8Zw==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "When Harry Met Sally");

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Ghostbusters ");

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Ghostbusters 2");

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 4,
                column: "Title",
                value: "Rio Bravo");
        }
    }
}
