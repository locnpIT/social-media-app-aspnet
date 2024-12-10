using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace social_media_app.Migrations
{
    /// <inheritdoc />
    public partial class RenameTableReels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reels_user_user_id",
                table: "Reels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reels",
                table: "Reels");

            migrationBuilder.RenameTable(
                name: "Reels",
                newName: "reels");

            migrationBuilder.RenameIndex(
                name: "IX_Reels_user_id",
                table: "reels",
                newName: "IX_reels_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reels",
                table: "reels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_reels_user_user_id",
                table: "reels",
                column: "user_id",
                principalTable: "user",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reels_user_user_id",
                table: "reels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reels",
                table: "reels");

            migrationBuilder.RenameTable(
                name: "reels",
                newName: "Reels");

            migrationBuilder.RenameIndex(
                name: "IX_reels_user_id",
                table: "Reels",
                newName: "IX_Reels_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reels",
                table: "Reels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reels_user_user_id",
                table: "Reels",
                column: "user_id",
                principalTable: "user",
                principalColumn: "Id");
        }
    }
}
