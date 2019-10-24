using Microsoft.EntityFrameworkCore.Migrations;

namespace asp_mvc.Migrations.ApplicationDb
{
    public partial class updateadminseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3a5a215-d738-4ad6-8411-a062e2513f5c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d12103e-25af-4652-a1d9-92d53caefa84", "001bed46-07dd-414c-b841-288e9d5ab044", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d12103e-25af-4652-a1d9-92d53caefa84");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3a5a215-d738-4ad6-8411-a062e2513f5c", "172ab0cd-97b1-4682-b4bc-5952282ca781", "Admin", "ADMIN" });
        }
    }
}
