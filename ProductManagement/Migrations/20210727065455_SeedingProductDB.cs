using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductManagement.Migrations
{
    public partial class SeedingProductDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Discount", "Infomation", "IsDeleted", "Photo", "Price", "ProductName", "PublishYear", "Quantity" },
                values: new object[] { 1, 1, 0m, "15.6 Inch, 1920 x 1080 Pixel, IPS, 60 Hz, 200 nits, Acer ComfyView LED-backlit", false, "/Images/Laptop-Acer.jpg", 13500000, "Laptop Acer Aspire 3 A315 56 502X i5 ", 2020, 10 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Discount", "Infomation", "IsDeleted", "Photo", "Price", "ProductName", "PublishYear", "Quantity" },
                values: new object[] { 2, 2, 0m, "6.7 Inch, Super Retina XDR, OLED, 2778 x 1284 Pixel", false, "/Images/Iphone12ProMax.jpg", 29900000, "iPhone 12 Pro Max 128GB ", 2020, 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);
        }
    }
}
