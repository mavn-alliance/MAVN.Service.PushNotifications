using System;

namespace Lykke.Service.PushNotifications.Client.Models.Requests
{
    /// <summary>
    /// Used to mark a message as read
    /// </summary>
    public class MarkMessageAsReadRequestModel
    {
        /// <summary>
        /// The message's group id
        /// </summary>
        public Guid MessageGroupId { get; set; }
    }
}
