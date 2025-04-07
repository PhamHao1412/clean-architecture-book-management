using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Movie.Migrations
{
    /// <inheritdoc />
    public partial class Create_Field_UserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "createAt", "updatedAt" },
                values: new object[] { new DateTime(2024, 6, 23, 12, 48, 26, 337, DateTimeKind.Utc).AddTicks(6248), new DateTime(2024, 6, 23, 12, 48, 26, 337, DateTimeKind.Utc).AddTicks(6249) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 6, 23, 12, 48, 26, 337, DateTimeKind.Utc).AddTicks(6268), new DateTime(2024, 6, 23, 12, 48, 26, 337, DateTimeKind.Utc).AddTicks(6269) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt", "lastLogged", "user_id" },
                values: new object[] { new DateTime(2024, 6, 23, 12, 48, 26, 337, DateTimeKind.Utc).AddTicks(6132), new DateTime(2024, 6, 23, 12, 48, 26, 337, DateTimeKind.Utc).AddTicks(6132), new DateTime(2024, 6, 23, 12, 48, 26, 337, DateTimeKind.Utc).AddTicks(6131), "Hao00123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "createAt", "updatedAt" },
                values: new object[] { new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6399), new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6417), new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6418) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt", "lastLogged" },
                values: new object[] { new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6268), new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6269), new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6268) });
        }
    }
}
