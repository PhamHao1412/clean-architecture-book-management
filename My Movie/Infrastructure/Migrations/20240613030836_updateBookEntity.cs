using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Movie.Migrations
{
    /// <inheritdoc />
    public partial class updateBookEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 6, 13, 3, 8, 35, 288, DateTimeKind.Utc).AddTicks(4025), new DateTime(2024, 6, 13, 3, 8, 35, 288, DateTimeKind.Utc).AddTicks(4024) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 6, 6, 4, 2, 26, 19, DateTimeKind.Utc).AddTicks(2296), new DateTime(2024, 6, 6, 4, 2, 26, 19, DateTimeKind.Utc).AddTicks(2295) });
        }
    }
}
