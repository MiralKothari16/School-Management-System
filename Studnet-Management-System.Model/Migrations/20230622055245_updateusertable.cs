using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studnet_Management_System.Model.Migrations
{
    /// <inheritdoc />
    public partial class updateusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegisterrdId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterrdId",
                table: "Users");
        }
    }
}
