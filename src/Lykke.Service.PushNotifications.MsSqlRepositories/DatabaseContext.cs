using System.Data.Common;
using JetBrains.Annotations;
using Lykke.Common.MsSql;
using Lykke.Service.PushNotifications.Domain;
using Lykke.Service.PushNotifications.MsSqlRepositories.Entities;
using Lykke.Service.PushNotifications.MsSqlRepositories.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Lykke.Service.PushNotifications.MsSqlRepositories
{
    public class DatabaseContext : MsSqlContext
    {
        private const string Schema = "push_notifications";

        public DbSet<PushNotificationRegistration> PushNotificationRegistrations { get; set; }
        public DbSet<NotificationMessage> NotificationMessages{ get; set; }

        // empty constructor needed for EF migrations
        [UsedImplicitly]
        public DatabaseContext() : base(Schema)
        {
        }

        public DatabaseContext(string connectionString, bool isTraceEnabled)
            : base(Schema, connectionString, isTraceEnabled)
        {
        }

        public DatabaseContext(DbConnection dbConnection)
            : base(Schema, dbConnection)
        {
        }

        protected override void OnLykkeModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PushNotificationRegistrationConfiguration());

            //NotificationMessage
            modelBuilder.Entity<NotificationMessage>()
                .HasIndex(b => b.CustomerId);
            modelBuilder.Entity<NotificationMessage>()
                .HasIndex(b => b.CreationTimestamp);
            modelBuilder.Entity<NotificationMessage>()
                .HasIndex(b => b.MessageGroupId)
                .IsUnique();
            modelBuilder.Entity<NotificationMessage>()
                .HasIndex(b => b.IsRead);
        }
    }
}
