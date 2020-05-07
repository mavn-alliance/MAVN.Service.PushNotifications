using System;
using Autofac;
using AutoMapper;
using MAVN.Service.PushNotifications.Domain.Services;
using MAVN.Common.Encryption;

namespace MAVN.Service.PushNotifications.DomainServices
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PushNotificationRegistrationService>()
                .As<IPushNotificationRegistrationService>()
                .SingleInstance();

            builder.RegisterType<NotificationMessageService>()
                .As<INotificationMessageService>()
                .SingleInstance();

            builder.RegisterType<Mapper>().As<IMapper>().SingleInstance();

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
