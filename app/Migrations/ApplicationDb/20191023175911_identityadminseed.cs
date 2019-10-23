using Microsoft.EntityFrameworkCore.Migrations;

namespace asp_mvc.Migrations.ApplicationDb
{
    public partial class identityadminseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3a5a215-d738-4ad6-8411-a062e2513f5c", "172ab0cd-97b1-4682-b4bc-5952282ca781", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3a5a215-d738-4ad6-8411-a062e2513f5c");
        }
    }
}
