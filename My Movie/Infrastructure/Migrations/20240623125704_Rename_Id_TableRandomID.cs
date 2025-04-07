using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Movie.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Id_TableRandomID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "random_id",
                table: "RandomIds",
                newName: "id");

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
                columns: new[] { "CreatedAt", "UpdatedAt", "lastLogged" },
                values: new object[] { new DateTime(2024, 6, 23, 12, 57, 4, 313, DateTimeKind.Utc).AddTicks(4974), new DateTime(2024, 6, 23, 12, 57, 4, 313, DateTimeKind.Utc).AddTicks(4975), new DateTime(2024, 6, 23, 12, 57, 4, 313, DateTimeKind.Utc).AddTicks(4974) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "RandomIds",
                newName: "random_id");

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
                columns: new[] { "CreatedAt", "UpdatedAt", "lastLogged" },
                values: new object[] { new DateTime(2024, 6, 23, 12, 48, 26, 337, DateTimeKind.Utc).AddTicks(6132), new DateTime(2024, 6, 23, 12, 48, 26, 337, DateTimeKind.Utc).AddTicks(6132), new DateTime(2024, 6, 23, 12, 48, 26, 337, DateTimeKind.Utc).AddTicks(6131) });
        }
    }
}
