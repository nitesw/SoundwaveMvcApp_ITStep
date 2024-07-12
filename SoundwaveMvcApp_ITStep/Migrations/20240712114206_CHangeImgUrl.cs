using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoundwaveMvcApp_ITStep.Migrations
{
    /// <inheritdoc />
    public partial class CHangeImgUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImgUrl",
                value: "https://preview.redd.it/o94pn5h60lz61.png?width=1080&crop=smart&auto=webp&s=7464db335ee53167d2f6e2288d162711ed0a31d1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImgUrl",
                value: "https://i.redd.it/lhg9d9b80lz61.png");
        }
    }
}
