using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanvasArtStore.Data.Migrations
{
    public partial class AddRefactoredUrls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: new Guid("3046ada7-50e1-4049-8243-4cd004a10728"));

            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: new Guid("4c3b0343-83e6-4e85-a8ed-bec189bcf135"));

            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: new Guid("fab5e517-8272-46cb-8ea2-e2ea65327cce"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Paintings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 29, 12, 13, 7, 995, DateTimeKind.Utc).AddTicks(5997),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 28, 19, 31, 36, 155, DateTimeKind.Utc).AddTicks(1561));

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Author", "BuyerId", "CategoryId", "CuratorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("0b5f1f12-1f33-4e86-b79e-08c5baef968e"), "Joel Filipe, Unsplash", new Guid("da6fa3e5-9921-4aae-8e9b-502ae65a27f1"), 1, new Guid("83da74c8-b268-4d7e-a527-65082dfce13d"), "This painting was made with some experimental liquids as milk, water paint and oil.", "https://bityl.co/JYUO", 1200.00m, "The Order of Chaos" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Author", "BuyerId", "CategoryId", "CuratorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("64a9703c-33a4-4089-b864-62cf3c52a465"), "Kseniya Lapteva", null, 3, new Guid("83da74c8-b268-4d7e-a527-65082dfce13d"), "Painting and HD Art Wallpaper", "https://bityl.co/JYUS", 2000.00m, "No Name Contemporary" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Author", "BuyerId", "CategoryId", "CuratorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("84ffcc1c-bd03-46f7-912d-2621dde04381"), "Eric Ravilious (d. 1942)", null, 2, new Guid("83da74c8-b268-4d7e-a527-65082dfce13d"), "Thanks to Birmingham Museums Trust, the UK.", "https://bityl.co/JYU5", 1200.00m, "The Tractor, 1933" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: new Guid("0b5f1f12-1f33-4e86-b79e-08c5baef968e"));

            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: new Guid("64a9703c-33a4-4089-b864-62cf3c52a465"));

            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: new Guid("84ffcc1c-bd03-46f7-912d-2621dde04381"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Paintings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 28, 19, 31, 36, 155, DateTimeKind.Utc).AddTicks(1561),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 29, 12, 13, 7, 995, DateTimeKind.Utc).AddTicks(5997));

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Author", "BuyerId", "CategoryId", "CreatedOn", "CuratorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("3046ada7-50e1-4049-8243-4cd004a10728"), "Kseniya Lapteva", null, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("83da74c8-b268-4d7e-a527-65082dfce13d"), "Painting and HD Art Wallpaper", "https://unsplash.com/photos/pHz2h_uK18Yg", 2000.00m, "No Name Contemporary" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Author", "BuyerId", "CategoryId", "CreatedOn", "CuratorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("4c3b0343-83e6-4e85-a8ed-bec189bcf135"), "Eric Ravilious (d. 1942)", null, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("83da74c8-b268-4d7e-a527-65082dfce13d"), "Thanks to Birmingham Museums Trust, the UK.", "https://unsplash.com/photos/l4AfiQ5-Mwc", 1200.00m, "The Tractor, 1933" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Author", "BuyerId", "CategoryId", "CreatedOn", "CuratorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("fab5e517-8272-46cb-8ea2-e2ea65327cce"), "Joel Filipe, Unsplash", new Guid("da6fa3e5-9921-4aae-8e9b-502ae65a27f1"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("83da74c8-b268-4d7e-a527-65082dfce13d"), "This painting was made with some experimental liquids as milk, water paint and oil.", "https://unsplash.com/photos/aYkDMrRo580", 1200.00m, "The Order of Chaos" });
        }
    }
}
