using System.ComponentModel.DataAnnotations;

namespace Lykke.Service.PushNotifications.Client.Models.Requests
{
    /// <summary>
    /// Used to get notification messages for a customer
    /// </summary>
    public class NotificationMessagesRequestModel : PaginatedRequestModel
    {
        /// <summary>
        /// The customer's id
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string CustomerId { get; set; }
    }
}
