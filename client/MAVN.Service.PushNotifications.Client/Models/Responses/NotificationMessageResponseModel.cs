using System;
using System.Collections.Generic;

namespace MAVN.Service.PushNotifications.Client.Models.Responses
{
    /// <summary>
    /// Represents a single notification message
    /// </summary>
    public class NotificationMessageResponseModel
    {
        /// <summary>
        /// The message group id
        /// </summary>
        public Guid MessageGroupId { get; set; }

        /// <summary>
        /// The sent timestamp
        /// </summary>
        public DateTime CreationTimestamp { get; set; }

        /// <summary>
        /// The message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Is message read
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// The custom payload
        /// </summary>
        public Dictionary<string, string> CustomPayload { get; set; }
    }
}
