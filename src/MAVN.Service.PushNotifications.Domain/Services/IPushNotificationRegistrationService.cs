using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.PushNotifications.Domain.Enums;

namespace MAVN.Service.PushNotifications.Domain.Services
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
