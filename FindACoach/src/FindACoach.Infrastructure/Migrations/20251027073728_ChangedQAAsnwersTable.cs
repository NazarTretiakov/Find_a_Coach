using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindACoach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedQAAsnwersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "QAAnswers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_QAAnswers_UserId",
                table: "QAAnswers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QAAnswers_AspNetUsers_UserId",
                table: "QAAnswers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QAAnswers_AspNetUsers_UserId",
                table: "QAAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QAAnswers_UserId",
                table: "QAAnswers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "QAAnswers");
        }
    }
}
