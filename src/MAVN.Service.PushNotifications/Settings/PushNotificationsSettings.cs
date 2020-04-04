using JetBrains.Annotations;

namespace MAVN.Service.PushNotifications.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PushNotificationsSettings
    {
        public DbSettings Db { get; set; }
    }
}
