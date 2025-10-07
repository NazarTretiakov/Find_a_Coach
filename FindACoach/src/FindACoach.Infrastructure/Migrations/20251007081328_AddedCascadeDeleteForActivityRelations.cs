using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindACoach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCascadeDeleteForActivityRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Activities_ActivityId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Activities_ActivityId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Saves_Activities_ActivityId",
                table: "Saves");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Activities_ActivityId",
                table: "Comments",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Activities_ActivityId",
                table: "Likes",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Saves_Activities_ActivityId",
                table: "Saves",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Activities_ActivityId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Activities_ActivityId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Saves_Activities_ActivityId",
                table: "Saves");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Activities_ActivityId",
                table: "Comments",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Activities_ActivityId",
                table: "Likes",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Saves_Activities_ActivityId",
                table: "Saves",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }
    }
}
