using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindACoach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedQAAnswersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QAAnswers_AspNetUsers_UserId",
                table: "QAAnswers");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "QAAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_QAAnswers_AspNetUsers_UserId",
                table: "QAAnswers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QAAnswers_AspNetUsers_UserId",
                table: "QAAnswers");

            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "QAAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_QAAnswers_AspNetUsers_UserId",
                table: "QAAnswers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
