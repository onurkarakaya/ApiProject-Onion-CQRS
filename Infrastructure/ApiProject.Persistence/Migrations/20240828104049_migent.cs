using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 40, 49, 803, DateTimeKind.Local).AddTicks(9613), "Health & Automotive" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 40, 49, 803, DateTimeKind.Local).AddTicks(9630), "Games & Health" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 40, 49, 803, DateTimeKind.Local).AddTicks(9635), "Outdoors" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 28, 13, 40, 49, 804, DateTimeKind.Local).AddTicks(1505));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 28, 13, 40, 49, 804, DateTimeKind.Local).AddTicks(1507));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 28, 13, 40, 49, 804, DateTimeKind.Local).AddTicks(1508));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 28, 13, 40, 49, 804, DateTimeKind.Local).AddTicks(1510));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 40, 49, 805, DateTimeKind.Local).AddTicks(4253), "Dolorem ducimus ekşili ışık sıfat.", "Labore." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 40, 49, 805, DateTimeKind.Local).AddTicks(4283), "Hesap oldular mi minima gülüyorum.", "Aperiam." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 40, 49, 805, DateTimeKind.Local).AddTicks(4372), "Koştum kapının quaerat ki qui.", "Accusantium." });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 40, 49, 807, DateTimeKind.Local).AddTicks(2712), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", 1.490648281471520m, 257.88m, "Awesome Cotton Tuna" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 40, 49, 807, DateTimeKind.Local).AddTicks(2742), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", 6.174146487141450m, 240.64m, "Ergonomic Cotton Ball" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2024, 8, 25, 13, 44, 56, 340, DateTimeKind.Local).AddTicks(6900), "Industrial, Music & Grocery" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2024, 8, 25, 13, 44, 56, 340, DateTimeKind.Local).AddTicks(6922), "Music & Movies" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Name" },
                values: new object[] { new DateTime(2024, 8, 25, 13, 44, 56, 340, DateTimeKind.Local).AddTicks(6930), "Garden" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 13, 44, 56, 341, DateTimeKind.Local).AddTicks(672));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 13, 44, 56, 341, DateTimeKind.Local).AddTicks(674));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 13, 44, 56, 341, DateTimeKind.Local).AddTicks(676));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 13, 44, 56, 341, DateTimeKind.Local).AddTicks(678));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTime(2024, 8, 25, 13, 44, 56, 343, DateTimeKind.Local).AddTicks(949), "Çakıl gördüm düşünüyor batarya camisi.", "Numquam." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTime(2024, 8, 25, 13, 44, 56, 343, DateTimeKind.Local).AddTicks(992), "Aut beğendim umut kulu layıkıyla.", "Bahar." });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description", "Title" },
                values: new object[] { new DateTime(2024, 8, 25, 13, 44, 56, 343, DateTimeKind.Local).AddTicks(1033), "Aliquid çarpan salladı çıktılar rem.", "Değirmeni." });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2024, 8, 25, 13, 44, 56, 347, DateTimeKind.Local).AddTicks(208), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", 1.753314746741820m, 665.50m, "Handcrafted Frozen Chicken" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[] { new DateTime(2024, 8, 25, 13, 44, 56, 347, DateTimeKind.Local).AddTicks(253), "The Football Is Good For Training And Recreational Purposes", 3.099200913272560m, 591.44m, "Handcrafted Granite Shoes" });
        }
    }
}
