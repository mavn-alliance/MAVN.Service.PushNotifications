using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.PushNotifications.MsSqlRepositories.Migrations
{
    public partial class AddedUniquePropertyToNotificationMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NotificationMessages_message_group_id",
                schema: "push_notifications",
                table: "NotificationMessages");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessages_message_group_id",
                schema: "push_notifications",
                table: "NotificationMessages",
                column: "message_group_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NotificationMessages_message_group_id",
                schema: "push_notifications",
                table: "NotificationMessages");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessages_message_group_id",
                schema: "push_notifications",
                table: "NotificationMessages",
                column: "message_group_id");
        }
    }
}
