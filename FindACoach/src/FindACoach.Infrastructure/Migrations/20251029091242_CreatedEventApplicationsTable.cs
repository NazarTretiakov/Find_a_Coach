using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindACoach.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedEventApplicationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SearchPersonPanelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventApplications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventApplications_SearchPeoplePanels_SearchPersonPanelId",
                        column: x => x.SearchPersonPanelId,
                        principalTable: "SearchPeoplePanels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventApplications_SearchPersonPanelId",
                table: "EventApplications",
                column: "SearchPersonPanelId");

            migrationBuilder.CreateIndex(
                name: "IX_EventApplications_UserId",
                table: "EventApplications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventApplications");
        }
    }
}
