using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDueDateInBorrowTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Borrows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDate",
                value: new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 20, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 8 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 13, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 23, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 9 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 15, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 9, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 10 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 30, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 8 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 21, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 2, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 18, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 10 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 18, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 2, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 8, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 9 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 13, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 8 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "BookId", "DueDate", "UserId" },
                values: new object[] { 29, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Utc), 10 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "Comment", "URL", "UserId" },
                values: new object[] { 28, "Nulla ex et fuga qui.", "https://picsum.photos/640/480/?image=19", 8 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 6, "Nisi quam sapiente ut earum esse et nesciunt.", 4, "https://picsum.photos/640/480/?image=485", 6 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "Comment", "URL", "UserId" },
                values: new object[] { 11, "Consequatur veritatis tempore molestiae at sit dolor ipsa.", "https://picsum.photos/640/480/?image=306", 4 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 23, "Officiis velit unde maiores aut.", 2, "https://picsum.photos/640/480/?image=727", 3 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 16, "Temporibus cupiditate a perspiciatis numquam placeat tempore et accusantium.", 3, "https://picsum.photos/640/480/?image=382", 2 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 19, "Nesciunt voluptatem sed vero est veritatis libero nostrum perspiciatis.", 3, "https://picsum.photos/640/480/?image=875", 3 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 17, "Ea nesciunt et autem corporis.", 4, "https://picsum.photos/640/480/?image=92", 7 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 27, "Error nostrum id magni facilis odit impedit et at.", 5, "https://picsum.photos/640/480/?image=980", 3 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 25, "Nihil dignissimos quia et reiciendis aliquid alias magni.", 1, "https://picsum.photos/640/480/?image=710", 8 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 10, "Aliquam quas laboriosam ut.", 5, "https://picsum.photos/640/480/?image=279", 2 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 13, "Praesentium sed facilis odio ut.", 2, "https://picsum.photos/640/480/?image=358", 3 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 7, "Voluptatem qui libero eos ipsa vel molestiae perspiciatis.", 3, "https://picsum.photos/640/480/?image=922", 5 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 14, "Omnis et blanditiis error excepturi eos sunt incidunt cum.", 2, "https://picsum.photos/640/480/?image=167", 8 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 11, "Ut qui non eveniet quis temporibus omnis porro repellendus.", 4, "https://picsum.photos/640/480/?image=1065", 4 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "BookId", "Comment", "URL", "UserId" },
                values: new object[] { 20, "Debitis unde at sed ipsum.", "https://picsum.photos/640/480/?image=323", 6 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "BookId", "Comment", "URL", "UserId" },
                values: new object[] { 27, "Porro doloribus a voluptatum rem perferendis quia aut ea cupiditate.", "https://picsum.photos/640/480/?image=558", 7 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "BookId", "Comment", "Rating", "URL" },
                values: new object[] { 3, "Et quia inventore eos officiis molestiae numquam suscipit labore.", 3, "https://picsum.photos/640/480/?image=274" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 4, "Aut maiores accusantium consequatur dolorum sit ullam eum beatae.", 1, "https://picsum.photos/640/480/?image=571", 4 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "BookId", "Comment", "URL", "UserId" },
                values: new object[] { 24, "Alias rerum repellendus exercitationem ipsam suscipit tempore nisi consequatur vel.", "https://picsum.photos/640/480/?image=283", 3 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 30, "Quia nisi corrupti dolor quasi et sed ex.", 4, "https://picsum.photos/640/480/?image=543", 6 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Borrows");

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 10, 7 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 23, 9 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 29, 5 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 30, 8 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 15, 7 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 18, 10 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 5, 6 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 8, 9 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 28, 5 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 28, 8 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 10, 10 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 19, 4 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 18, 5 });

            migrationBuilder.UpdateData(
                table: "Borrows",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 20, 5 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "Comment", "URL", "UserId" },
                values: new object[] { 13, "Esse et nesciunt nisi suscipit veniam mollitia est consequatur veritatis.", "https://picsum.photos/640/480/?image=1041", 4 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 13, "Ipsa minima placeat libero ex suscipit officiis velit unde.", 2, "https://picsum.photos/640/480/?image=927", 7 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "Comment", "URL", "UserId" },
                values: new object[] { 30, "Molestias nulla temporibus cupiditate a.", "https://picsum.photos/640/480/?image=134", 10 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 7, "Accusantium magnam et consequatur natus dolor nesciunt.", 4, "https://picsum.photos/640/480/?image=814", 6 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 4, "Libero nostrum perspiciatis.", 4, "https://picsum.photos/640/480/?image=929", 4 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 17, "Ea nesciunt et autem corporis.", 4, "https://picsum.photos/640/480/?image=92", 7 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 27, "Error nostrum id magni facilis odit impedit et at.", 5, "https://picsum.photos/640/480/?image=980", 3 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 25, "Nihil dignissimos quia et reiciendis aliquid alias magni.", 1, "https://picsum.photos/640/480/?image=710", 8 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 10, "Aliquam quas laboriosam ut.", 5, "https://picsum.photos/640/480/?image=279", 2 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 13, "Praesentium sed facilis odio ut.", 2, "https://picsum.photos/640/480/?image=358", 3 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 7, "Voluptatem qui libero eos ipsa vel molestiae perspiciatis.", 3, "https://picsum.photos/640/480/?image=922", 5 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 14, "Omnis et blanditiis error excepturi eos sunt incidunt cum.", 2, "https://picsum.photos/640/480/?image=167", 8 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 11, "Ut qui non eveniet quis temporibus omnis porro repellendus.", 4, "https://picsum.photos/640/480/?image=1065", 4 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 20, "Debitis unde at sed ipsum.", 3, "https://picsum.photos/640/480/?image=323", 6 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "BookId", "Comment", "URL", "UserId" },
                values: new object[] { 27, "Porro doloribus a voluptatum rem perferendis quia aut ea cupiditate.", "https://picsum.photos/640/480/?image=558", 7 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "BookId", "Comment", "URL", "UserId" },
                values: new object[] { 3, "Et quia inventore eos officiis molestiae numquam suscipit labore.", "https://picsum.photos/640/480/?image=274", 4 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "BookId", "Comment", "Rating", "URL" },
                values: new object[] { 4, "Aut maiores accusantium consequatur dolorum sit ullam eum beatae.", 1, "https://picsum.photos/640/480/?image=571" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 24, "Alias rerum repellendus exercitationem ipsam suscipit tempore nisi consequatur vel.", 4, "https://picsum.photos/640/480/?image=283", 3 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "BookId", "Comment", "URL", "UserId" },
                values: new object[] { 30, "Quia nisi corrupti dolor quasi et sed ex.", "https://picsum.photos/640/480/?image=543", 6 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "BookId", "Comment", "Rating", "URL", "UserId" },
                values: new object[] { 19, "Et vel commodi est neque quia suscipit nostrum.", 3, "https://picsum.photos/640/480/?image=386", 1 });
        }
    }
}
