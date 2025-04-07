using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Movie.Migrations
{
    /// <inheritdoc />
    public partial class udapte_CreateAtUpdateAt_for_UserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserBooks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "createAt",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 6, 6, 4, 2, 26, 19, DateTimeKind.Utc).AddTicks(2296), new DateTime(2024, 6, 6, 4, 2, 26, 19, DateTimeKind.Utc).AddTicks(2295) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "createAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "Roles");

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedAt",
                value: new DateTime(2024, 6, 5, 8, 56, 1, 608, DateTimeKind.Utc).AddTicks(8290));
        }
    }
}
