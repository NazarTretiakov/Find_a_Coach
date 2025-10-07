using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindACoach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedLikesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Activities_ActivityId",
                table: "Likes");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Activities_ActivityId",
                table: "Likes",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Activities_ActivityId",
                table: "Likes");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Activities_ActivityId",
                table: "Likes",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
