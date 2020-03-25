using System;
using JetBrains.Annotations;

namespace Lykke.Service.PushNotifications.Client
{
    /// <summary>
    /// PushNotifications client interface.
    /// </summary>
    [PublicAPI]
    public interface IPushNotificationsClient
    {
        /// <summary>Application Api interface</summary>
        [Obsolete("This interface is obsolete and will be removed in the future." +
                  " Use PushRegistrationsApi instead.", true)]
        IPushNotificationsApi Api { get; }

        /// <summary>Application PushRegistrationsApi interface</summary>
        IPushRegistrationsApi PushRegistrationsApi { get; }

        /// <summary>Application NotificationMessagesApi interface</summary>
        INotificationMessagesApi NotificationMessagesApi { get; }
    }
}
