using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrinkMachine.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastAccessed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Coins",
                columns: new[] { "Id", "ImageUrl", "IsBlocked", "Value" },
                values: new object[,]
                {
                    { 1, "https://ru-moneta.ru/upload/monety-ru/rubl-1-1.jpg", false, 1 },
                    { 2, "https://ru-moneta.ru/upload/monety-ru/st-1-4.jpg", false, 2 },
                    { 3, "https://ru-moneta.ru/upload/monety-ru/1999-sr.jpg", false, 5 },
                    { 4, "https://ru-moneta.ru/upload/moneta-ru/2019-10rub-01r.jpg", false, 10 }
                });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "https://cdn.iportal.ru/preview/news/articles/1abee4eeb5843831b81b268032278c150f1c079a_1200_800_c.jpg", "Pepsi", 15, 10 },
                    { 2, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR0cdopkxHgUUq2rrnqIVRMCXYbyQD0f2n7-A&usqp=CAU", "Fanta", 17, 15 },
                    { 3, "https://lenta.servicecdn.ru/globalassets/1/-/65/86/05/438102.png?preset=fulllossywhite", "Dobriy Cola", 12, 121 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "DbSessions");

            migrationBuilder.DropTable(
                name: "Drinks");
        }
    }
}
