using System;
using System.Collections.Generic;

namespace Lykke.Service.PushNotifications.Domain.Contracts
{
    public class NotificationMessage
    {
        public string MessageGroupId { get; set; }

        public DateTime CreationTimestamp { get; set; }

        public string Message { get; set; }

        public bool IsRead { get; set; }

        public Dictionary<string, string> CustomPayload { get; set; }
    }
}
