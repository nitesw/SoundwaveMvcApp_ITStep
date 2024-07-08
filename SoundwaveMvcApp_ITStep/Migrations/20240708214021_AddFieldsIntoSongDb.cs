using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoundwaveMvcApp_ITStep.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsIntoSongDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1,
                column: "UploadDate",
                value: new DateTime(2024, 7, 9, 0, 40, 20, 612, DateTimeKind.Local).AddTicks(4089));

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "AdditionalTags", "ArtistName", "Description", "GenreId", "IsPublic", "SongUrl", "Title", "UploadDate", "UserId" },
                values: new object[] { 2, null, null, null, 1, false, "aaa.com/mp3", "Test Song 2", new DateTime(2024, 7, 9, 0, 40, 20, 612, DateTimeKind.Local).AddTicks(4136), 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1,
                column: "UploadDate",
                value: new DateTime(2024, 7, 9, 0, 15, 27, 370, DateTimeKind.Local).AddTicks(5323));
        }
    }
}
