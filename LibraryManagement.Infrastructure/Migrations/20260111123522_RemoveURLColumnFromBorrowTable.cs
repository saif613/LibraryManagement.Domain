using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveURLColumnFromBorrowTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "Borrows");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Borrows",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 1,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=833");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 2,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=458");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 3,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=317");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 4,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=322");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 5,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=218");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 6,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=38");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 7,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=160");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 8,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=45");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 9,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=815");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 10,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=1043");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 11,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=19");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 12,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=917");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 13,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=675");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 14,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=189");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 15,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=747");
        }
    }
}
