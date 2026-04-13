using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Comment", "CreatedAt", "Rating", "URL", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 13, "Esse et nesciunt nisi suscipit veniam mollitia est consequatur veritatis.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, "https://picsum.photos/640/480/?image=1041", null, 4 },
                    { 2, 13, "Ipsa minima placeat libero ex suscipit officiis velit unde.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "https://picsum.photos/640/480/?image=927", null, 7 },
                    { 3, 30, "Molestias nulla temporibus cupiditate a.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "https://picsum.photos/640/480/?image=134", null, 10 },
                    { 4, 7, "Accusantium magnam et consequatur natus dolor nesciunt.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "https://picsum.photos/640/480/?image=814", null, 6 },
                    { 5, 4, "Libero nostrum perspiciatis.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "https://picsum.photos/640/480/?image=929", null, 4 },
                    { 6, 17, "Ea nesciunt et autem corporis.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "https://picsum.photos/640/480/?image=92", null, 7 },
                    { 7, 27, "Error nostrum id magni facilis odit impedit et at.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, "https://picsum.photos/640/480/?image=980", null, 3 },
                    { 8, 25, "Nihil dignissimos quia et reiciendis aliquid alias magni.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "https://picsum.photos/640/480/?image=710", null, 8 },
                    { 9, 10, "Aliquam quas laboriosam ut.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, "https://picsum.photos/640/480/?image=279", null, 2 },
                    { 10, 13, "Praesentium sed facilis odio ut.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "https://picsum.photos/640/480/?image=358", null, 3 },
                    { 11, 7, "Voluptatem qui libero eos ipsa vel molestiae perspiciatis.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "https://picsum.photos/640/480/?image=922", null, 5 },
                    { 12, 14, "Omnis et blanditiis error excepturi eos sunt incidunt cum.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "https://picsum.photos/640/480/?image=167", null, 8 },
                    { 13, 11, "Ut qui non eveniet quis temporibus omnis porro repellendus.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "https://picsum.photos/640/480/?image=1065", null, 4 },
                    { 14, 20, "Debitis unde at sed ipsum.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "https://picsum.photos/640/480/?image=323", null, 6 },
                    { 15, 27, "Porro doloribus a voluptatum rem perferendis quia aut ea cupiditate.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "https://picsum.photos/640/480/?image=558", null, 7 },
                    { 16, 3, "Et quia inventore eos officiis molestiae numquam suscipit labore.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "https://picsum.photos/640/480/?image=274", null, 4 },
                    { 17, 4, "Aut maiores accusantium consequatur dolorum sit ullam eum beatae.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "https://picsum.photos/640/480/?image=571", null, 4 },
                    { 18, 24, "Alias rerum repellendus exercitationem ipsam suscipit tempore nisi consequatur vel.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "https://picsum.photos/640/480/?image=283", null, 3 },
                    { 19, 30, "Quia nisi corrupti dolor quasi et sed ex.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "https://picsum.photos/640/480/?image=543", null, 6 },
                    { 20, 19, "Et vel commodi est neque quia suscipit nostrum.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "https://picsum.photos/640/480/?image=386", null, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
