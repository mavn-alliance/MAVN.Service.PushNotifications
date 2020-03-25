using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.PushNotifications.Client 
{
    /// <summary>
    /// PushNotifications client settings.
    /// </summary>
    public class PushNotificationsServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
