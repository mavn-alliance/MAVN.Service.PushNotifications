using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.PushNotifications.Domain.Contracts;

namespace MAVN.Service.PushNotifications.Domain.Services
{
    public interface INotificationMessageService
    {
        Task ProcessNotificationMessageAsync(
            Guid messageId,
            string pushRegistrationId,
            string messageGroupId,
            string customerId,
            string message,
            string customPayload,
            Dictionary<string, string> messageParameters);

        Task<PaginatedList<NotificationMessage>> GetNotificationMessagesForCustomerAsync(string customerId, int currentPage, int pageSize);

        Task MarkMessageAsReadAsync(string messageGroupId);

        Task MarkAllMessagesAsReadAsync(string customerId);

        Task<int> GetUnreadMessagesCountAsync(string customerId);
    }
}
