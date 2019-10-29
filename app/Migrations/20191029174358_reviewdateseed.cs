using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asp_mvc.Migrations
{
    public partial class reviewdateseed : Migration
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

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Article", "ReviewDate" },
                values: new object[] { "/pFoc0IJdqJ+5qNa1plKVBPopxwpyP3T7AGrYNdkKpw=", new DateTime(1989, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Article", "MovieID", "ReviewDate" },
                values: new object[] { 2, "NQKO7CTPRboG+CceiapXoD38YHs9WOk2kKwMZG676Ns=", 1, new DateTime(1989, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

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

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Article", "ReviewDate" },
                values: new object[] { "ZMhZSRV/DXqjBTa6ekvAAICpqePIZcD+JqpSuqCpVUI=", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
