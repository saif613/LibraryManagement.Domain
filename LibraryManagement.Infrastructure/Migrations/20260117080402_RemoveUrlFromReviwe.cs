using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUrlFromReviwe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "Reviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Reviews",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=19");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=485");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=306");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=727");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=382");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=875");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=92");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 8,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=980");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 9,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=710");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 10,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=279");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 11,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=358");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 12,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=922");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 13,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=167");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 14,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=1065");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 15,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=323");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 16,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=558");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 17,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=274");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 18,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=571");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 19,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=283");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 20,
                column: "URL",
                value: "https://picsum.photos/640/480/?image=543");
        }
    }
}
