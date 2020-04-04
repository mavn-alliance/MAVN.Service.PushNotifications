using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PushNotifications.MsSqlRepositories.Migrations
{
    public partial class RenamedSentTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sent_timestamp",
                schema: "push_notifications",
                table: "NotificationMessages",
                newName: "creation_timestamp");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationMessages_sent_timestamp",
                schema: "push_notifications",
                table: "NotificationMessages",
                newName: "IX_NotificationMessages_creation_timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "creation_timestamp",
                schema: "push_notifications",
                table: "NotificationMessages",
                newName: "sent_timestamp");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationMessages_creation_timestamp",
                schema: "push_notifications",
                table: "NotificationMessages",
                newName: "IX_NotificationMessages_sent_timestamp");
        }
    }
}
