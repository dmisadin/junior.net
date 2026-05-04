using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbySalto.Junior.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Customers
            migrationBuilder.InsertData(
                table: "Customers",
                columns: ["FirstName", "LastName", "ContactNumber", "DeliveryAddress"],
                columnTypes: ["nvarchar(50)", "nvarchar(50)", "nvarchar(20)", "nvarchar(250)"],
                values: new object[,]
                {
                    { "Ivan",     "Horvat",     "+385911234567", "Ilica 1, Zagreb" },
                    { "Marija",   "Kovač",      "+385922345678", "Vukovarska 2, Split" },
                    { "Tomislav", "Babić",      "+385933456789", "Kapucinska 3, Osijek" },
                    { "Ana",      "Novak",      "+385944567890", "Korzo 4, Rijeka" },
                    { "Petra",    "Jurić",      "+385955678901", "Široka 5, Dubrovnik" },
                    { "Luka",     "Blašković",  "+385966789012", "Frankopanska 6, Varaždin" },
                    { "Nina",     "Šimić",      "+385977890123", "Stjepana Radića 7, Zadar" },
                    { "Mateo",    "Filipović",  "+385988901234", "Zadarska 8, Šibenik" },
                    { "Ivana",    "Maričić",    "+385919012345", "Vladimira Nazora 9, Karlovac" },
                    { "Josip",    "Knežević",   "+385900123456", "Ante Starčevića 10, Sisak" }
                }
            );

            // Products
            migrationBuilder.InsertData(
                table: "Products",
                columns: ["Name", "Price", "IsAvailable"],
                columnTypes: ["nvarchar(100)", "decimal(18,2)", "bit"],
                values: new object[,]
                {
                    { "Margherita Pizza",    8.50m,  true },
                    { "Pepperoni Pizza",     10.00m, true },
                    { "Caesar Salad",        6.50m,  true },
                    { "Chicken Burger",      9.00m,  true },
                    { "French Fries",        3.50m,  true },
                    { "Grilled Salmon",      14.00m, true },
                    { "Spaghetti Carbonara", 11.00m, true },
                    { "Tiramisu",            5.00m,  true },
                    { "Coca Cola 0.33l",     2.50m,  true },
                    { "Still Water 0.5l",    1.50m,  true }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
            migrationBuilder.Sql("DELETE FROM Customers");
        }
    }
}
