using System;
using Autofac;
using AutoMapper;
using MAVN.Common.Encryption;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.PushNotifications.Domain.Repositories;
using MAVN.Service.PushNotifications.MsSqlRepositories.Repositories;

namespace MAVN.Service.PushNotifications.MsSqlRepositories
{
    public class AutofacModule : Module
    {
        private readonly string _connectionString;

        public AutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mapper>().As<IMapper>().SingleInstance();

            builder.RegisterPostgreSQL(
                _connectionString,
                connString => new DatabaseContext(connString, false),
                dbConn => new DatabaseContext(dbConn));

            builder.RegisterType<PushNotificationRegistrationRepository>()
                .As<IPushNotificationRegistrationRepository>()
                .SingleInstance();

            builder.RegisterType<NotificationMessageRepository>()
                .As<INotificationMessageRepository>()
                .SingleInstance();

            var encryptionKey = Environment.GetEnvironmentVariable("EncryptionKey");
            var encryptionIv = Environment.GetEnvironmentVariable("EncryptionIV");

            var serializer = new AesSerializer(encryptionKey, encryptionIv);
            builder.RegisterInstance(serializer)
                .As<IAesSerializer>()
                .SingleInstance();

            builder.RegisterType<EncryptionService>()
                .As<IEncryptionService>()
                .SingleInstance();
        }
    }
}
