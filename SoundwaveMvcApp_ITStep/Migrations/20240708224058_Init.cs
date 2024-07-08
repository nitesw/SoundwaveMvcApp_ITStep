using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoundwaveMvcApp_ITStep.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Playlists = table.Column<int>(type: "int", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    AdditionalTags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArtistName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tracks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "None" },
                    { 2, "Alternative Rock" },
                    { 3, "Ambient" },
                    { 4, "Classical" },
                    { 5, "Country" },
                    { 6, "Dance & EDM" },
                    { 7, "Dancehall" },
                    { 8, "Deep House" },
                    { 9, "Disco" },
                    { 10, "Drum & Bass" },
                    { 11, "Dubstep" },
                    { 12, "Electronic" },
                    { 13, "Folk & Singer-Songwriter" },
                    { 14, "Hip-hop & Rap" },
                    { 15, "House" },
                    { 16, "Indie" },
                    { 17, "Jazz & Blues" },
                    { 18, "Latin" },
                    { 19, "Metal" },
                    { 20, "Piano" },
                    { 21, "Pop" },
                    { 22, "R&B & Soul" },
                    { 23, "Reggae" },
                    { 24, "Reggaeton" },
                    { 25, "Rock" },
                    { 26, "Soundtrack" },
                    { 27, "Techno" },
                    { 28, "Trance" },
                    { 29, "Trap" },
                    { 30, "Triphop" },
                    { 31, "World" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Likes", "Password", "Playlists", "Username" },
                values: new object[,]
                {
                    { 1, "admin@mail.com", true, 0, "adminpass", 0, "admin" },
                    { 2, "damnsss@mail.com", false, 0, "1234pass", 1, "damnsss" },
                    { 3, "uzibook@mail.com", false, 12, "passsword1234", 2, "uzibook" },
                    { 4, "zxcnewr@mail.com", false, 0, "pass1234", 0, "zxcnewr" },
                    { 5, "moomaszh@mail.com", false, 5, "pass0000", 0, "Moomaszh" }
                });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AdditionalTags", "ArtistName", "Description", "GenreId", "IsArchived", "IsPublic", "Title", "TrackUrl", "UploadDate", "UserId" },
                values: new object[,]
                {
                    { 1, "true, tags", "Me", "True test music", 2, false, true, "Test Song", "randomsite.com/songurl.mp3", new DateTime(2024, 7, 9, 1, 40, 58, 2, DateTimeKind.Local).AddTicks(3837), 1 },
                    { 2, null, null, null, 1, false, false, "Test Song 2", "aaa.com/mp3", new DateTime(2024, 7, 9, 1, 40, 58, 2, DateTimeKind.Local).AddTicks(3881), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_GenreId",
                table: "Tracks",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_Title",
                table: "Tracks",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_UserId",
                table: "Tracks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
