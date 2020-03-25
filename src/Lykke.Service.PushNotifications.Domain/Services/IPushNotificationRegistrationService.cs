using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.PushNotifications.Domain.Enums;

namespace Lykke.Service.PushNotifications.Domain.Services
{
    public interface IPushNotificationRegistrationService
    {
        Task<PushTokenInsertionResult> RegisterForPushNotificationsAsync(PushNotificationRegistration data);

        Task<List<PushNotificationRegistration>> GetAllPushNotificationRegistrationsAsync();

        Task<List<PushNotificationRegistration>> GetAllPushNotificationRegistrationsForCustomerAsync(string customerId);

        Task DeletePushNotificationRegistrationAsync(Guid pushRegistrationId);

        Task DeleteRegistrationByTokenAsync(string token);

        Task DeleteAllPushNotificationRegistrationsForCustomerAsync(string customerId);
    }
}
