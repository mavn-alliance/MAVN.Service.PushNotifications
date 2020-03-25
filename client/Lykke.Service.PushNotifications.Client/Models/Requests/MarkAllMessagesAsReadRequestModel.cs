namespace Lykke.Service.PushNotifications.Client.Models.Requests
{
    /// <summary>
    /// Used to mark all messages as read
    /// </summary>
    public class MarkAllMessagesAsReadRequestModel
    {
        /// <summary>
        /// The customer for which to mark messages as read
        /// </summary>
        public string CustomerId { get; set; }
    }
}
