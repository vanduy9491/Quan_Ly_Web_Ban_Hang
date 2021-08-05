using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductManagement.Migrations
{
    public partial class SeedingUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c0c6661b-0964-4e62-8083-3cac6a6741ec", "1", "SystemAdmin", "SystemAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "32ffd287-205f-43a2-9f0d-80bc5309fb47", "2", "Customer", "Customer" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2c0fca4e-9376-4a70-bcc6-35bebe497866", 0, "/Images/bg-1.jpg", "24761eed-d9cf-4e48-9d7c-948a6d6fef27", null, false, false, null, "vanduy9491@gmail.com", "vanduy9491@gmail.com", "AQAAAAEAACcQAAAAECYnrRfHDMN72/ufgNFF38ibeQH0LPmpMNmSI8b7zfz7uFvqyPwr3QNB6CPXifoCFQ==", null, false, "0322ad8c-90ae-4484-aec2-d9ec0cc0952a", false, "vanduy9491@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "2c0fca4e-9376-4a70-bcc6-35bebe497866", "c0c6661b-0964-4e62-8083-3cac6a6741ec" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32ffd287-205f-43a2-9f0d-80bc5309fb47");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "2c0fca4e-9376-4a70-bcc6-35bebe497866", "c0c6661b-0964-4e62-8083-3cac6a6741ec" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0c6661b-0964-4e62-8083-3cac6a6741ec");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c0fca4e-9376-4a70-bcc6-35bebe497866");
        }
    }
}
