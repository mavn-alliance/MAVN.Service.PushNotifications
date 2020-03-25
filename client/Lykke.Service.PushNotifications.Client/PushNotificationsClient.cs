using Lykke.HttpClientGenerator;

namespace Lykke.Service.PushNotifications.Client
{
    /// <summary>
    /// PushNotifications API aggregating interface.
    /// </summary>
    public class PushNotificationsClient : IPushNotificationsClient
    {
        /// <summary>Interface to PushNotifications Api.</summary>
        public IPushNotificationsApi Api { get; private set; }

        /// <summary>Application PushRegistrationsApi interface</summary>
        public IPushRegistrationsApi PushRegistrationsApi { get; }

        /// <summary>Application NotificationMessagesApi interface</summary>
        public INotificationMessagesApi NotificationMessagesApi { get; }

        /// <summary>C-tor</summary>
        public PushNotificationsClient(IHttpClientGenerator httpClientGenerator)
        {
            Api = httpClientGenerator.Generate<IPushNotificationsApi>();
            PushRegistrationsApi = httpClientGenerator.Generate<IPushRegistrationsApi>();
            NotificationMessagesApi = httpClientGenerator.Generate<INotificationMessagesApi>();
        }
    }
}
