using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studnet_Management_System.Model.Migrations
{
    /// <inheritdoc />
    public partial class changesRTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RTokens_Users_UserId",
                table: "RTokens");

            migrationBuilder.DropIndex(
                name: "IX_RTokens_UserId",
                table: "RTokens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RTokens");

            migrationBuilder.CreateIndex(
                name: "IX_RTokens_User_Id",
                table: "RTokens",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RTokens_Users_User_Id",
                table: "RTokens",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RTokens_Users_User_Id",
                table: "RTokens");

            migrationBuilder.DropIndex(
                name: "IX_RTokens_User_Id",
                table: "RTokens");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "RTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RTokens_UserId",
                table: "RTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RTokens_Users_UserId",
                table: "RTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
