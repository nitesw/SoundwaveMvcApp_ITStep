using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIntermediateClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistTrack_Playlists_PlaylistsId",
                table: "PlaylistTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistTrack_Tracks_TracksId",
                table: "PlaylistTrack");

            migrationBuilder.RenameColumn(
                name: "TracksId",
                table: "PlaylistTrack",
                newName: "TrackId");

            migrationBuilder.RenameColumn(
                name: "PlaylistsId",
                table: "PlaylistTrack",
                newName: "PlaylistId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistTrack_TracksId",
                table: "PlaylistTrack",
                newName: "IX_PlaylistTrack_TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistTrack_Playlists_PlaylistId",
                table: "PlaylistTrack",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistTrack_Tracks_TrackId",
                table: "PlaylistTrack",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistTrack_Playlists_PlaylistId",
                table: "PlaylistTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistTrack_Tracks_TrackId",
                table: "PlaylistTrack");

            migrationBuilder.RenameColumn(
                name: "TrackId",
                table: "PlaylistTrack",
                newName: "TracksId");

            migrationBuilder.RenameColumn(
                name: "PlaylistId",
                table: "PlaylistTrack",
                newName: "PlaylistsId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistTrack_TrackId",
                table: "PlaylistTrack",
                newName: "IX_PlaylistTrack_TracksId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistTrack_Playlists_PlaylistsId",
                table: "PlaylistTrack",
                column: "PlaylistsId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistTrack_Tracks_TracksId",
                table: "PlaylistTrack",
                column: "TracksId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
