using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindACoach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSearchPersonPanelsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SearchPersonPanelSkill_Skills_PrefferedSkillsId",
                table: "SearchPersonPanelSkill");

            migrationBuilder.RenameColumn(
                name: "PrefferedSkillsId",
                table: "SearchPersonPanelSkill",
                newName: "PreferredSkillsId");

            migrationBuilder.RenameIndex(
                name: "IX_SearchPersonPanelSkill_PrefferedSkillsId",
                table: "SearchPersonPanelSkill",
                newName: "IX_SearchPersonPanelSkill_PreferredSkillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SearchPersonPanelSkill_Skills_PreferredSkillsId",
                table: "SearchPersonPanelSkill",
                column: "PreferredSkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SearchPersonPanelSkill_Skills_PreferredSkillsId",
                table: "SearchPersonPanelSkill");

            migrationBuilder.RenameColumn(
                name: "PreferredSkillsId",
                table: "SearchPersonPanelSkill",
                newName: "PrefferedSkillsId");

            migrationBuilder.RenameIndex(
                name: "IX_SearchPersonPanelSkill_PreferredSkillsId",
                table: "SearchPersonPanelSkill",
                newName: "IX_SearchPersonPanelSkill_PrefferedSkillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SearchPersonPanelSkill_Skills_PrefferedSkillsId",
                table: "SearchPersonPanelSkill",
                column: "PrefferedSkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
