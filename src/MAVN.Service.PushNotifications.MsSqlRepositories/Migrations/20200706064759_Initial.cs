using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PushNotifications.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "push_notifications");

            migrationBuilder.CreateTable(
                name: "NotificationMessages",
                schema: "push_notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    customer_id = table.Column<string>(maxLength: 50, nullable: false),
                    message_group_id = table.Column<string>(maxLength: 50, nullable: false),
                    creation_timestamp = table.Column<DateTime>(nullable: false),
                    custom_payload = table.Column<string>(maxLength: 4000, nullable: false),
                    message = table.Column<string>(maxLength: 10000, nullable: false),
                    is_read = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMessages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "push_notification_registrations",
                schema: "push_notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<string>(maxLength: 50, nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    PushRegistrationToken = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_push_notification_registrations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessages_creation_timestamp",
                schema: "push_notifications",
                table: "NotificationMessages",
                column: "creation_timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessages_customer_id",
                schema: "push_notifications",
                table: "NotificationMessages",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessages_is_read",
                schema: "push_notifications",
                table: "NotificationMessages",
                column: "is_read");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessages_message_group_id",
                schema: "push_notifications",
                table: "NotificationMessages",
                column: "message_group_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_push_notification_registrations_CustomerId",
                schema: "push_notifications",
                table: "push_notification_registrations",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationMessages",
                schema: "push_notifications");

            migrationBuilder.DropTable(
                name: "push_notification_registrations",
                schema: "push_notifications");
        }
    }
}
