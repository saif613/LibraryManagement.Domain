using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialLibraryDbSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Borrows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Borrowed"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Borrows_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Borrows_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "ROLE_STAMP_1", "Admin", "ADMIN" },
                    { 2, "ROLE_STAMP_2", "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sunt recusandae molestias et quia expedita culpa doloribus enim occaecati.", "Shoes", null },
                    { 2, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Quia dolor est id facilis ipsam autem molestias.", "Industrial", null },
                    { 3, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Unde rerum distinctio velit quidem iure.", "Baby", null },
                    { 4, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sapiente qui aspernatur.", "Games", null },
                    { 5, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Non similique fugit ab.", "Grocery", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "USER_CONCUR_1", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tito_Hessel@hotmail.com", true, true, null, "Filomena Ledner", "TITO_HESSEL@HOTMAIL.COM", "TITO_HESSEL@HOTMAIL.COM", "AQAAAAIAAYagAAAAEO9v9p6X3GjS5I5f6m4g5r9k5l5t5y5u5i5o5p5", null, false, "USER_STAMP_1", false, null, "Tito_Hessel@hotmail.com" },
                    { 2, 0, "USER_CONCUR_2", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Herman.Rohan@gmail.com", true, true, null, "Devyn Hermiston", "HERMAN.ROHAN@GMAIL.COM", "HERMAN.ROHAN@GMAIL.COM", "AQAAAAIAAYagAAAAEO9v9p6X3GjS5I5f6m4g5r9k5l5t5y5u5i5o5p5", null, false, "USER_STAMP_2", false, null, "Herman.Rohan@gmail.com" },
                    { 3, 0, "USER_CONCUR_3", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gerson.Mueller@gmail.com", true, true, null, "Della Hahn", "GERSON.MUELLER@GMAIL.COM", "GERSON.MUELLER@GMAIL.COM", "AQAAAAIAAYagAAAAEO9v9p6X3GjS5I5f6m4g5r9k5l5t5y5u5i5o5p5", null, false, "USER_STAMP_3", false, null, "Gerson.Mueller@gmail.com" },
                    { 4, 0, "USER_CONCUR_4", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dedrick_Lynch@hotmail.com", true, true, null, "Aiyana Dietrich", "DEDRICK_LYNCH@HOTMAIL.COM", "DEDRICK_LYNCH@HOTMAIL.COM", "AQAAAAIAAYagAAAAEO9v9p6X3GjS5I5f6m4g5r9k5l5t5y5u5i5o5p5", null, false, "USER_STAMP_4", false, null, "Dedrick_Lynch@hotmail.com" },
                    { 5, 0, "USER_CONCUR_5", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Caterina43@yahoo.com", true, true, null, "Noel MacGyver", "CATERINA43@YAHOO.COM", "CATERINA43@YAHOO.COM", "AQAAAAIAAYagAAAAEO9v9p6X3GjS5I5f6m4g5r9k5l5t5y5u5i5o5p5", null, false, "USER_STAMP_5", false, null, "Caterina43@yahoo.com" },
                    { 6, 0, "USER_CONCUR_6", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Clair_Heathcote28@yahoo.com", true, true, null, "Winona Reynolds", "CLAIR_HEATHCOTE28@YAHOO.COM", "CLAIR_HEATHCOTE28@YAHOO.COM", "AQAAAAIAAYagAAAAEO9v9p6X3GjS5I5f6m4g5r9k5l5t5y5u5i5o5p5", null, false, "USER_STAMP_6", false, null, "Clair_Heathcote28@yahoo.com" },
                    { 7, 0, "USER_CONCUR_7", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kelley.Abbott13@gmail.com", true, true, null, "Antonette Rogahn", "KELLEY.ABBOTT13@GMAIL.COM", "KELLEY.ABBOTT13@GMAIL.COM", "AQAAAAIAAYagAAAAEO9v9p6X3GjS5I5f6m4g5r9k5l5t5y5u5i5o5p5", null, false, "USER_STAMP_7", false, null, "Kelley.Abbott13@gmail.com" },
                    { 8, 0, "USER_CONCUR_8", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Alize12@hotmail.com", true, true, null, "Renee White", "ALIZE12@HOTMAIL.COM", "ALIZE12@HOTMAIL.COM", "AQAAAAIAAYagAAAAEO9v9p6X3GjS5I5f6m4g5r9k5l5t5y5u5i5o5p5", null, false, "USER_STAMP_8", false, null, "Alize12@hotmail.com" },
                    { 9, 0, "USER_CONCUR_9", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ewell_Kuvalis25@hotmail.com", true, true, null, "Marlen Schmitt", "EWELL_KUVALIS25@HOTMAIL.COM", "EWELL_KUVALIS25@HOTMAIL.COM", "AQAAAAIAAYagAAAAEO9v9p6X3GjS5I5f6m4g5r9k5l5t5y5u5i5o5p5", null, false, "USER_STAMP_9", false, null, "Ewell_Kuvalis25@hotmail.com" },
                    { 10, 0, "USER_CONCUR_10", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dewitt.McClure@hotmail.com", true, true, null, "Dakota Collins", "DEWITT.MCCLURE@HOTMAIL.COM", "DEWITT.MCCLURE@HOTMAIL.COM", "AQAAAAIAAYagAAAAEO9v9p6X3GjS5I5f6m4g5r9k5l5t5y5u5i5o5p5", null, false, "USER_STAMP_10", false, null, "Dewitt.McClure@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 10 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CategoryId", "CreatedAt", "ISBN", "StockQuantity", "Title", "URL", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Myrtice Casper", 5, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "0995764628211", 31, "Gorgeous Metal Car", "https://picsum.photos/640/480/?image=713", null },
                    { 2, "Hope Kuhlman", 1, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "2404055566098", 11, "Fantastic Cotton Shirt", "https://picsum.photos/640/480/?image=646", null },
                    { 3, "Leilani Thiel", 5, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "3104986968822", 13, "Handmade Soft Pants", "https://picsum.photos/640/480/?image=696", null },
                    { 4, "Kassandra Cremin", 1, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "0805725451508", 28, "Awesome Fresh Towels", "https://picsum.photos/640/480/?image=843", null },
                    { 5, "Johnpaul Dietrich", 5, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "9598773334824", 23, "Small Cotton Tuna", "https://picsum.photos/640/480/?image=871", null },
                    { 6, "Lillian O'Keefe", 1, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "3145285018122", 18, "Awesome Wooden Shoes", "https://picsum.photos/640/480/?image=65", null },
                    { 7, "Nikko Bayer", 5, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "9110332375897", 43, "Sleek Fresh Bike", "https://picsum.photos/640/480/?image=37", null },
                    { 8, "Kristy Willms", 4, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "2224351423693", 29, "Refined Wooden Table", "https://picsum.photos/640/480/?image=794", null },
                    { 9, "Chet Thompson", 5, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "5135912243346", 40, "Intelligent Steel Pants", "https://picsum.photos/640/480/?image=685", null },
                    { 10, "Mohammad Runte", 2, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "1704992416878", 36, "Handcrafted Frozen Chicken", "https://picsum.photos/640/480/?image=258", null },
                    { 11, "Jovanny Dach", 4, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "3999913970234", 21, "Small Fresh Chicken", "https://picsum.photos/640/480/?image=693", null },
                    { 12, "Laurel Zieme", 3, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "9708552948825", 28, "Licensed Wooden Sausages", "https://picsum.photos/640/480/?image=162", null },
                    { 13, "Hyman Schamberger", 2, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "6271441895898", 27, "Intelligent Fresh Computer", "https://picsum.photos/640/480/?image=83", null },
                    { 14, "Hunter Johnston", 2, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "9359291094900", 41, "Generic Fresh Keyboard", "https://picsum.photos/640/480/?image=528", null },
                    { 15, "Gaston Aufderhar", 5, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "4498982638377", 1, "Handcrafted Steel Sausages", "https://picsum.photos/640/480/?image=712", null },
                    { 16, "Steve Kuphal", 5, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "3084684215599", 21, "Gorgeous Soft Cheese", "https://picsum.photos/640/480/?image=818", null },
                    { 17, "Alaina Hintz", 4, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "7571124490822", 44, "Sleek Cotton Chicken", "https://picsum.photos/640/480/?image=50", null },
                    { 18, "Corrine Bruen", 4, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "3947239203819", 27, "Fantastic Rubber Table", "https://picsum.photos/640/480/?image=800", null },
                    { 19, "Elias Hoppe", 1, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "4030629876904", 46, "Handmade Plastic Tuna", "https://picsum.photos/640/480/?image=503", null },
                    { 20, "Shanna Stark", 4, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "2145275519098", 42, "Licensed Wooden Chips", "https://picsum.photos/640/480/?image=150", null },
                    { 21, "Angeline Barton", 1, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "4053486859637", 23, "Incredible Rubber Pants", "https://picsum.photos/640/480/?image=850", null },
                    { 22, "Reuben Walker", 3, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "6377508626273", 43, "Gorgeous Rubber Cheese", "https://picsum.photos/640/480/?image=1015", null },
                    { 23, "Brooks Klein", 2, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "1791371674783", 7, "Intelligent Rubber Car", "https://picsum.photos/640/480/?image=580", null },
                    { 24, "Prince Kautzer", 4, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "9068783908648", 19, "Small Plastic Bacon", "https://picsum.photos/640/480/?image=671", null },
                    { 25, "Gene Shanahan", 3, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "8187832753884", 36, "Tasty Cotton Towels", "https://picsum.photos/640/480/?image=290", null },
                    { 26, "Jarod Koss", 3, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "3168832133673", 49, "Rustic Wooden Keyboard", "https://picsum.photos/640/480/?image=471", null },
                    { 27, "Zetta Harris", 3, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "6546982986825", 46, "Licensed Wooden Pizza", "https://picsum.photos/640/480/?image=89", null },
                    { 28, "Tom MacGyver", 5, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "1041713217025", 8, "Handcrafted Granite Chair", "https://picsum.photos/640/480/?image=149", null },
                    { 29, "Sean Anderson", 1, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "0326436558206", 22, "Licensed Soft Chips", "https://picsum.photos/640/480/?image=1016", null },
                    { 30, "Vallie Terry", 3, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "4971661916954", 46, "Generic Wooden Ball", "https://picsum.photos/640/480/?image=173", null }
                });

            migrationBuilder.InsertData(
                table: "Borrows",
                columns: new[] { "Id", "BookId", "BorrowDate", "CreatedAt", "ReturnDate", "URL", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 23, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=833", null, 9 },
                    { 2, 10, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=458", null, 7 },
                    { 3, 23, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=317", null, 9 },
                    { 4, 29, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=322", null, 5 },
                    { 5, 30, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=218", null, 8 },
                    { 6, 15, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=38", null, 7 },
                    { 7, 18, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=160", null, 10 },
                    { 8, 5, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=45", null, 6 },
                    { 9, 8, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=815", null, 9 },
                    { 10, 28, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=1043", null, 5 },
                    { 11, 28, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=19", null, 8 },
                    { 12, 10, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=917", null, 10 },
                    { 13, 19, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=675", null, 4 },
                    { 14, 18, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=189", null, 5 },
                    { 15, 20, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "https://picsum.photos/640/480/?image=747", null, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_BookId",
                table: "Borrows",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_UserId",
                table: "Borrows",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Borrows");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
