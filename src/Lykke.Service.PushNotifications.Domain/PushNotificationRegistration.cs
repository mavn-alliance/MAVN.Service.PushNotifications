using System;

namespace Lykke.Service.PushNotifications.Domain
{
    public class PushNotificationRegistration
    {
        public Guid Id { get; set; }

        public string CustomerId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string PushRegistrationToken { get; set; }
    }
}
