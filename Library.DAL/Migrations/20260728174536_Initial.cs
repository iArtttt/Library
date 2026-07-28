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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<int>(type: "int", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    PublisherTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Taken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WhenReturned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToReturn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_BorrowedBooks_Users_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "LastName", "Name", "SecondName" },
                values: new object[,]
                {
                    { new Guid("10ce119e-0cf5-478f-a016-964530f3c330"), "Fiom", "Oleg", null },
                    { new Guid("1f2f09f8-5326-4f16-81a2-81705a3406ea"), "Syropin", "Ivan", "Grozniy" },
                    { new Guid("a36ded85-bf17-4815-adf1-ca2f07b81930"), "Syropin", "Vasiliy", "Krot" }
                });

            migrationBuilder.InsertData(
                table: "PublishingCodeTypes",
                columns: new[] { "Id", "PublisherCode" },
                values: new object[,]
                {
                    { new Guid("19ccc910-4fcf-413e-85ec-ae8803f8788d"), "ISRC" },
                    { new Guid("4f881976-6c99-469e-ab6d-8bb9ade69d15"), "ISBN" },
                    { new Guid("8590aa52-7cbe-4d1c-9b10-ed4f8637957e"), "ISWC" },
                    { new Guid("e93a6306-87c8-43f1-9b98-018e4428561c"), "ISSN" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birthday", "DocumentNumber", "DocumentType", "Email", "LastName", "Login", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("010a3fc9-d742-41d3-becd-f4f2669fc2c3"), null, null, null, "admin@gmail.com", "Svichkar", "Admin", "Artur", "1234", 2 },
                    { new Guid("575bad15-19aa-4616-90d7-718006dce32c"), null, null, null, "admin1@gmail.com", "Sunches", "Admin1", "Rick", "4567", 2 },
                    { new Guid("670ec28c-274b-4009-8f5d-637206220341"), new DateTime(1993, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "777789", 0, "rEAr@gmail.com", "Zeroph", "Reader1", "Alex", "1423", 1 },
                    { new Guid("7d74a99a-bd3d-42e7-a461-9cc65bc26626"), new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Local), "3354213", 1, "reader@gmail.com", "Lighter", "Reader", "Bob", "1234", 1 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "City", "Count", "Country", "Genre", "Name", "PublishYear", "PublisherTypeId", "ReturnedDays" },
                values: new object[,]
                {
                    { new Guid("5ebc58c4-2618-4348-b3ea-bf9bbc5f3a03"), "Kiev", 5, "Ukrain", 32, "Summer Time", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("19ccc910-4fcf-413e-85ec-ae8803f8788d"), 30 },
                    { new Guid("ac61b44e-c484-4566-bfc0-13f6804b9c59"), null, 2, "Poland", 2, "Mgla", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4f881976-6c99-469e-ab6d-8bb9ade69d15"), 30 },
                    { new Guid("eb886598-4b81-4e2f-b11c-ba4b32fa5ed0"), "Kiev", 1, "Ukrain", 16, "World Story", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e93a6306-87c8-43f1-9b98-018e4428561c"), 30 },
                    { new Guid("f527f881-937f-42bf-89b1-02df6c19e8cd"), "Kharkiv", 2, "Ukrain", 1024, "C# for smart", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4f881976-6c99-469e-ab6d-8bb9ade69d15"), 30 }
                });

            migrationBuilder.InsertData(
                table: "AuthorBook",
                columns: new[] { "AuthorsId", "BooksId" },
                values: new object[,]
                {
                    { new Guid("10ce119e-0cf5-478f-a016-964530f3c330"), new Guid("eb886598-4b81-4e2f-b11c-ba4b32fa5ed0") },
                    { new Guid("10ce119e-0cf5-478f-a016-964530f3c330"), new Guid("f527f881-937f-42bf-89b1-02df6c19e8cd") },
                    { new Guid("1f2f09f8-5326-4f16-81a2-81705a3406ea"), new Guid("eb886598-4b81-4e2f-b11c-ba4b32fa5ed0") },
                    { new Guid("a36ded85-bf17-4815-adf1-ca2f07b81930"), new Guid("5ebc58c4-2618-4348-b3ea-bf9bbc5f3a03") },
                    { new Guid("a36ded85-bf17-4815-adf1-ca2f07b81930"), new Guid("ac61b44e-c484-4566-bfc0-13f6804b9c59") }
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
                name: "Users");

            migrationBuilder.DropTable(
                name: "PublishingCodeTypes");
        }
    }
}
