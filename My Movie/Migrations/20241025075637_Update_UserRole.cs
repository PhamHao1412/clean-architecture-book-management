using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Movie.Migrations
{
    /// <inheritdoc />
    public partial class Update_UserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "createAt", "updatedAt" },
                values: new object[] { new DateTime(2024, 10, 25, 7, 56, 37, 490, DateTimeKind.Utc).AddTicks(5828), new DateTime(2024, 10, 25, 7, 56, 37, 490, DateTimeKind.Utc).AddTicks(5828) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "createAt", "updatedAt" },
                values: new object[] { 2, "User", new DateTime(2024, 10, 25, 7, 56, 37, 490, DateTimeKind.Utc).AddTicks(5844), new DateTime(2024, 10, 25, 7, 56, 37, 490, DateTimeKind.Utc).AddTicks(5844) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 25, 7, 56, 37, 490, DateTimeKind.Utc).AddTicks(5860), new DateTime(2024, 10, 25, 7, 56, 37, 490, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt", "lastLogged", "user_id" },
                values: new object[] { new DateTime(2024, 10, 25, 7, 56, 37, 490, DateTimeKind.Utc).AddTicks(5642), new DateTime(2024, 10, 25, 7, 56, 37, 490, DateTimeKind.Utc).AddTicks(5642), new DateTime(2024, 10, 25, 7, 56, 37, 490, DateTimeKind.Utc).AddTicks(5642), "Admin_00123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "createAt", "updatedAt" },
                values: new object[] { new DateTime(2024, 6, 23, 12, 57, 4, 313, DateTimeKind.Utc).AddTicks(5099), new DateTime(2024, 6, 23, 12, 57, 4, 313, DateTimeKind.Utc).AddTicks(5099) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 6, 23, 12, 57, 4, 313, DateTimeKind.Utc).AddTicks(5118), new DateTime(2024, 6, 23, 12, 57, 4, 313, DateTimeKind.Utc).AddTicks(5118) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt", "lastLogged", "user_id" },
                values: new object[] { new DateTime(2024, 6, 23, 12, 57, 4, 313, DateTimeKind.Utc).AddTicks(4974), new DateTime(2024, 6, 23, 12, 57, 4, 313, DateTimeKind.Utc).AddTicks(4975), new DateTime(2024, 6, 23, 12, 57, 4, 313, DateTimeKind.Utc).AddTicks(4974), "Hao00123" });
        }
    }
}
