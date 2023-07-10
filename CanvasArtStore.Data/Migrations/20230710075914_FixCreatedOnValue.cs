using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanvasArtStore.Data.Migrations
{
    public partial class FixCreatedOnValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 29, 12, 13, 7, 995, DateTimeKind.Utc).AddTicks(5997));

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Author", "BuyerId", "CategoryId", "CuratorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("62995100-ec96-405c-94f1-ae6b9be7d7f1"), "Eric Ravilious (d. 1942)", null, 2, new Guid("83da74c8-b268-4d7e-a527-65082dfce13d"), "Thanks to Birmingham Museums Trust, the UK.", "https://bityl.co/JYU5", 1200.00m, "The Tractor, 1933" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Author", "BuyerId", "CategoryId", "CuratorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("6e2bf39c-8242-4964-86d2-2b531fa4548d"), "Kseniya Lapteva", null, 3, new Guid("83da74c8-b268-4d7e-a527-65082dfce13d"), "Painting and HD Art Wallpaper", "https://bityl.co/JYUS", 2000.00m, "No Name Contemporary" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Author", "BuyerId", "CategoryId", "CuratorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("9fced0bb-bedc-419b-a80c-01ef32751efe"), "Joel Filipe, Unsplash", new Guid("da6fa3e5-9921-4aae-8e9b-502ae65a27f1"), 1, new Guid("83da74c8-b268-4d7e-a527-65082dfce13d"), "This painting was made with some experimental liquids as milk, water paint and oil.", "https://bityl.co/JYUO", 1200.00m, "The Order of Chaos" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: new Guid("62995100-ec96-405c-94f1-ae6b9be7d7f1"));

            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: new Guid("6e2bf39c-8242-4964-86d2-2b531fa4548d"));

            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: new Guid("9fced0bb-bedc-419b-a80c-01ef32751efe"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Paintings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 29, 12, 13, 7, 995, DateTimeKind.Utc).AddTicks(5997),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Author", "BuyerId", "CategoryId", "CreatedOn", "CuratorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("0b5f1f12-1f33-4e86-b79e-08c5baef968e"), "Joel Filipe, Unsplash", new Guid("da6fa3e5-9921-4aae-8e9b-502ae65a27f1"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("83da74c8-b268-4d7e-a527-65082dfce13d"), "This painting was made with some experimental liquids as milk, water paint and oil.", "https://bityl.co/JYUO", 1200.00m, "The Order of Chaos" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Author", "BuyerId", "CategoryId", "CreatedOn", "CuratorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("64a9703c-33a4-4089-b864-62cf3c52a465"), "Kseniya Lapteva", null, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("83da74c8-b268-4d7e-a527-65082dfce13d"), "Painting and HD Art Wallpaper", "https://bityl.co/JYUS", 2000.00m, "No Name Contemporary" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Author", "BuyerId", "CategoryId", "CreatedOn", "CuratorId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("84ffcc1c-bd03-46f7-912d-2621dde04381"), "Eric Ravilious (d. 1942)", null, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("83da74c8-b268-4d7e-a527-65082dfce13d"), "Thanks to Birmingham Museums Trust, the UK.", "https://bityl.co/JYU5", 1200.00m, "The Tractor, 1933" });
        }
    }
}
