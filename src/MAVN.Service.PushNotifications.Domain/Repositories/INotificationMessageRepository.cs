using System;
using System.Threading.Tasks;
using MAVN.Service.PushNotifications.Domain.Contracts;

namespace MAVN.Service.PushNotifications.Domain.Repositories
{
    public interface INotificationMessageRepository
    {
        Task<bool> CheckIfMessageExistsAsync(string messageGroupId, string customerId);

        Task CreateAsync(string customerId, string messageGroupId, string customPayload, string encryptedMessage);

        Task<PaginatedList<NotificationMessage>> GetNotificationMessagesForCustomerAsync(int skip, int take,
            string customerId);

        Task MarkMessageAsReadAsync(string messageGroupId);

        Task MarkAllMessagesAsReadAsync(string customerId);

        Task<int> GetUnreadMessagesCountAsync(string customerId);
    }
}
