using JetBrains.Annotations;

namespace Lykke.Service.PushNotifications.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PushNotificationsSettings
    {
        public DbSettings Db { get; set; }
    }
}
