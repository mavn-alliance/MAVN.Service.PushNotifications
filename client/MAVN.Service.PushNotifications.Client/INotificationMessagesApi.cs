using System.Threading.Tasks;
using MAVN.Service.PushNotifications.Client.Models.Requests;
using MAVN.Service.PushNotifications.Client.Models.Responses;
using Refit;

namespace MAVN.Service.PushNotifications.Client
{
    /// <summary>
    /// NotificationMessages client API interface.
    /// </summary>
    public interface INotificationMessagesApi
    {
        /// <summary>
        /// Gets notification messages for customer
        /// </summary>
        /// <param name="model">model containing push registration info</param>
        /// <returns>Details on registration outcome</returns>
        [Get("/api/notificationMessages")]
        Task<PaginatedResponseModel<NotificationMessageResponseModel>> GetNotificationMessagesForCustomerAsync(
            [Query] NotificationMessagesRequestModel model);

        /// <summary>
        /// Marks a notification message as read
        /// </summary>
        /// <param name="model">Information about the message</param>
        [Post("/api/notificationMessages/read")]
        Task MarkMessageAsReadAsync([Body] MarkMessageAsReadRequestModel model);

        /// <summary>
        /// Marks all notification messages as read
        /// </summary>
        /// <param name="model">Model containing information for marking as read</param>
        [Post("/api/notificationMessages/read/all")]
        Task MarkAllMessagesAsReadAsync([Body] MarkAllMessagesAsReadRequestModel model);

        /// <summary>
        /// Gets the count of all unread messages
        /// </summary>
        /// <param name="customerId">The customer's id</param>
        /// <returns>Count</returns>
        [Get("/api/notificationMessages/unread/count")]
        Task<int> GetUnreadMessagesCountAsync([Query] string customerId);
    }
}
