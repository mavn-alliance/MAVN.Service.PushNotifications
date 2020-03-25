using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.PushNotifications.MsSqlRepositories.Migrations
{
    public partial class ColumnsChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InfobipToken",
                schema: "push_notifications",
                table: "push_notification_registrations",
                newName: "PushRegistrationToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PushRegistrationToken",
                schema: "push_notifications",
                table: "push_notification_registrations",
                newName: "InfobipToken");
        }
    }
}
