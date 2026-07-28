using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublishingCodeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishingCodeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    PublisherTypeId = table.Column<int>(type: "int", nullable: false),
                    PublishYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReturnedDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_PublishingCodeTypes_PublisherTypeId",
                        column: x => x.PublisherTypeId,
                        principalTable: "PublishingCodeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Librarians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Librarians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Librarians_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Readers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    BooksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowedBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ReaderId = table.Column<int>(type: "int", nullable: false),
                    Taken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WhenReturned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToReturn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    LibrarianId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Librarians_LibrarianId",
                        column: x => x.LibrarianId,
                        principalTable: "Librarians",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Readers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "LastName", "Name", "SecondName" },
                values: new object[,]
                {
                    { 1, "Fiom", "Oleg", null },
                    { 2, "Syropin", "Ivan", "Grozniy" },
                    { 3, "Syropin", "Vasiliy", "Krot" }
                });

            migrationBuilder.InsertData(
                table: "PublishingCodeTypes",
                columns: new[] { "Id", "PublisherCode" },
                values: new object[,]
                {
                    { 1, "ISBN" },
                    { 2, "ISSN" },
                    { 3, "ISRC" },
                    { 4, "ISWC" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Login", "Password" },
                values: new object[,]
                {
                    { -4, "rEAr@gmail.com", "Reader1", "1423" },
                    { -3, "reader@gmail.com", "Reader", "1234" },
                    { -2, "admin1@gmail.com", "Admin1", "4567" },
                    { -1, "admin@gmail.com", "Admin", "1234" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "City", "Count", "Country", "Genre", "Name", "PublishYear", "PublisherTypeId", "ReturnedDays" },
                values: new object[,]
                {
                    { 1, "Kharkiv", 2, "Ukrain", 1024, "C# for smart", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 30 },
                    { 2, "Kiev", 1, "Ukrain", 16, "World Story", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 30 },
                    { 3, "Kiev", 5, "Ukrain", 32, "Summer Time", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 30 },
                    { 4, null, 2, "Poland", 2, "Mgla", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 30 }
                });

            migrationBuilder.InsertData(
                table: "Librarians",
                column: "Id",
                values: new object[]
                {
                    -2,
                    -1
                });

            migrationBuilder.InsertData(
                table: "Readers",
                columns: new[] { "Id", "Birthday", "DocumentNumber", "DocumentType", "LastName", "Name" },
                values: new object[,]
                {
                    { -4, new DateTime(1993, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "777789", 0, "Zeroph", "Alex" },
                    { -3, new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Local), "3354213", 1, "Lighter", "Bob" }
                });

            migrationBuilder.InsertData(
                table: "AuthorBook",
                columns: new[] { "AuthorsId", "BooksId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherTypeId",
                table: "Books",
                column: "PublisherTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_LibrarianId",
                table: "BorrowedBooks",
                column: "LibrarianId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_ReaderId",
                table: "BorrowedBooks",
                column: "ReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "BorrowedBooks");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Librarians");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropTable(
                name: "PublishingCodeTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
