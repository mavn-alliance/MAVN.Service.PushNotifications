using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PushNotifications.MsSqlRepositories.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "push_notifications");

            migrationBuilder.CreateTable(
                name: "push_notification_registrations",
                schema: "push_notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    CustomerId = table.Column<string>(maxLength: 50, nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    InfobipToken = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_push_notification_registrations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_push_notification_registrations_CustomerId",
                schema: "push_notifications",
                table: "push_notification_registrations",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "push_notification_registrations",
                schema: "push_notifications");
        }
    }
}
