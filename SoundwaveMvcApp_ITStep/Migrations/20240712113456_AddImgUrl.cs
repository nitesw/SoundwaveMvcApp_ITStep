using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoundwaveMvcApp_ITStep.Migrations
{
    /// <inheritdoc />
    public partial class AddImgUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Tracks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImgUrl", "UploadDate" },
                values: new object[] { "https://i.redd.it/lhg9d9b80lz61.png", new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImgUrl", "UploadDate" },
                values: new object[] { "https://i.redd.it/lhg9d9b80lz61.png", new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Tracks");

            migrationBuilder.UpdateData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: 1,
                column: "UploadDate",
                value: new DateTime(2024, 7, 9, 1, 40, 58, 2, DateTimeKind.Local).AddTicks(3837));

            migrationBuilder.UpdateData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: 2,
                column: "UploadDate",
                value: new DateTime(2024, 7, 9, 1, 40, 58, 2, DateTimeKind.Local).AddTicks(3881));
        }
    }
}
