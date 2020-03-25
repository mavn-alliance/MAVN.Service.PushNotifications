using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.PushNotifications.MsSqlRepositories.Migrations
{
    public partial class AddNotificationMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationMessages",
                schema: "push_notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    customer_id = table.Column<string>(maxLength: 50, nullable: false),
                    message_group_id = table.Column<string>(maxLength: 50, nullable: false),
                    sent_timestamp = table.Column<DateTime>(nullable: false),
                    custom_payload = table.Column<string>(maxLength: 4000, nullable: false),
                    message = table.Column<string>(maxLength: 10000, nullable: false),
                    is_read = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMessages", x => x.id);
                });

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
                column: "message_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessages_sent_timestamp",
                schema: "push_notifications",
                table: "NotificationMessages",
                column: "sent_timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationMessages",
                schema: "push_notifications");
        }
    }
}
