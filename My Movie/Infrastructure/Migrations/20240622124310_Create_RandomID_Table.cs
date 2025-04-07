using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace My_Movie.Migrations
{
    /// <inheritdoc />
    public partial class Create_RandomID_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddedAt",
                table: "UserRoles",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "AddedAt",
                table: "UserBooks",
                newName: "CreatedAt");

            migrationBuilder.CreateTable(
                name: "RandomIds",
                columns: table => new
                {
                    random_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    isUse = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandomIds", x => x.random_id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "createAt", "updatedAt" },
                values: new object[] { 1, "Administrator", new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6399), new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[] { new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6417), new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6418), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "LoginName", "Name", "Password", "UpdatedAt", "lastLogged" },
                values: new object[] { 1, new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6268), "admin", "Admin", "123", new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6269), new DateTime(2024, 6, 22, 12, 43, 9, 745, DateTimeKind.Utc).AddTicks(6268) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RandomIds");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "UserRoles",
                newName: "AddedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "UserBooks",
                newName: "AddedAt");

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AddedAt", "UpdatedAt", "UserId" },
                values: new object[] { new DateTime(2024, 6, 13, 3, 8, 35, 288, DateTimeKind.Utc).AddTicks(4025), new DateTime(2024, 6, 13, 3, 8, 35, 288, DateTimeKind.Utc).AddTicks(4024), 6 });
        }
    }
}
