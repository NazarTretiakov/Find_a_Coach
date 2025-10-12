using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindACoach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationBetweenSchoolsAndSkillsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Schools_SchoolId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_SchoolId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "SchoolSkill",
                columns: table => new
                {
                    SchoolsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSkill", x => new { x.SchoolsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_SchoolSkill_Schools_SchoolsId",
                        column: x => x.SchoolsId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolSkill_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSkill_SkillsId",
                table: "SchoolSkill",
                column: "SkillsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolSkill");

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SchoolId",
                table: "Skills",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Schools_SchoolId",
                table: "Skills",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id");
        }
    }
}
