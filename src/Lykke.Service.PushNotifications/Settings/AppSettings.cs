using JetBrains.Annotations;
using Lykke.Sdk.Settings;

namespace Lykke.Service.PushNotifications.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public PushNotificationsSettings PushNotificationsService { get; set; }

        public RabbitMqSettings Rabbit { get; set; }
    }
}
