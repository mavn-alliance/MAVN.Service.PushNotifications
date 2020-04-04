using MAVN.Service.PushNotifications.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAVN.Service.PushNotifications.MsSqlRepositories.EntityConfigurations
{
    public class PushNotificationRegistrationConfiguration : IEntityTypeConfiguration<PushNotificationRegistration>
    {
        public void Configure(EntityTypeBuilder<PushNotificationRegistration> builder)
        {
            builder.ToTable("push_notification_registrations");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValueSql("newid()");

            builder.Property(x => x.CustomerId).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.CustomerId).IsUnique(false);

            builder.Property(x => x.RegistrationDate).IsRequired().HasDefaultValueSql("SYSUTCDATETIME()")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.PushRegistrationToken).IsRequired().HasMaxLength(255);
        }
    }
}
