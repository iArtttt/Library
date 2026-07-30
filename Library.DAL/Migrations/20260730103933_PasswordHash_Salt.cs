using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.DAL.Migrations
{
    /// <inheritdoc />
    public partial class PasswordHash_Salt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("010a3fc9-d742-41d3-becd-f4f2669fc2c3"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "2EE3924D6BC599A379BCE90E975781395D0AA21E761EB1E35781311D532F0120", "A1B2C3D4E5F67890ABCDEF1234567890ABCDEF1234567890ABCDEF1234567890" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("575bad15-19aa-4616-90d7-718006dce32c"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "12729E7147B031D9AFCA9D2290408A91B953566EDCDB798416897426730A0AD9", "A1B2C3D4E5F67890ABCDEF1234567890ABCDEF1234567890ABCDEF1234567890" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("670ec28c-274b-4009-8f5d-637206220341"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "060E707990C6C0D74D41C62A36D52971BECF8B315B632C19A2B214350EC2D69B", "F9E8D7C6B5A43210FEDCBA0987654321FEDCBA0987654321FEDCBA0987654321" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7d74a99a-bd3d-42e7-a461-9cc65bc26626"),
                columns: new[] { "Birthday", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Local), "1FC25FB8D28DFB19D8633E24EC57D6ED9036C7AB708615F2919E1ADB8A31CBB8", "F9E8D7C6B5A43210FEDCBA0987654321FEDCBA0987654321FEDCBA0987654321" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("010a3fc9-d742-41d3-becd-f4f2669fc2c3"),
                column: "Password",
                value: "1234");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("575bad15-19aa-4616-90d7-718006dce32c"),
                column: "Password",
                value: "4567");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("670ec28c-274b-4009-8f5d-637206220341"),
                column: "Password",
                value: "1423");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7d74a99a-bd3d-42e7-a461-9cc65bc26626"),
                columns: new[] { "Birthday", "Password" },
                values: new object[] { new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Local), "1234" });
        }
    }
}
