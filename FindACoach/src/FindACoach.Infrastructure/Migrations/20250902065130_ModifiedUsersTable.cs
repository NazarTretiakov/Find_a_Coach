using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindACoach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompleteProfileWindowTitle",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleteProfileWindowVisible",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteProfileWindowTitle",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsCompleteProfileWindowVisible",
                table: "AspNetUsers");
        }
    }
}
