using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoundwaveMvcApp_ITStep.Migrations
{
    /// <inheritdoc />
    public partial class AddArchivedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Songs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsArchived", "UploadDate" },
                values: new object[] { false, new DateTime(2024, 7, 9, 0, 50, 35, 246, DateTimeKind.Local).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsArchived", "UploadDate" },
                values: new object[] { false, new DateTime(2024, 7, 9, 0, 50, 35, 246, DateTimeKind.Local).AddTicks(449) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Songs");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1,
                column: "UploadDate",
                value: new DateTime(2024, 7, 9, 0, 40, 20, 612, DateTimeKind.Local).AddTicks(4089));

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2,
                column: "UploadDate",
                value: new DateTime(2024, 7, 9, 0, 40, 20, 612, DateTimeKind.Local).AddTicks(4136));
        }
    }
}
