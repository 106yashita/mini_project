using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagementAPI.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledEvents_UserProfiles_UserId",
                table: "ScheduledEvents");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledEvents_UserId",
                table: "ScheduledEvents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ScheduledEvents");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledEvents_UserProfileId",
                table: "ScheduledEvents",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledEvents_UserProfiles_UserProfileId",
                table: "ScheduledEvents",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledEvents_UserProfiles_UserProfileId",
                table: "ScheduledEvents");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledEvents_UserProfileId",
                table: "ScheduledEvents");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ScheduledEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledEvents_UserId",
                table: "ScheduledEvents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledEvents_UserProfiles_UserId",
                table: "ScheduledEvents",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
