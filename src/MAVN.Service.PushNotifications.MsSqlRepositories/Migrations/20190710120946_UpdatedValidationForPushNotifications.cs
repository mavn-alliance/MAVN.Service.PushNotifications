using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PushNotifications.MsSqlRepositories.Migrations
{
    public partial class UpdatedValidationForPushNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InfobipToken",
                schema: "push_notifications",
                table: "push_notification_registrations",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InfobipToken",
                schema: "push_notifications",
                table: "push_notification_registrations",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }
    }
}
