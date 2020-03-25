using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.PushNotifications.Client.Enums;
using Lykke.Service.PushNotifications.Client.Models.Requests;
using Lykke.Service.PushNotifications.Client.Models.Responses;
using Refit;

namespace Lykke.Service.PushNotifications.Client
{
    /// <summary>
    /// PushNotifications client API interface.
    /// </summary>
    [PublicAPI]
    public interface IPushRegistrationsApi
    {
        /// <summary>
        /// Create new push registration
        /// </summary>
        /// <param name="model">model containing push registration info</param>
        /// <returns>Details on registration outcome</returns>
        [Post("/api/pushRegistrations")]
        Task<PushTokenInsertionResult> RegisterForPushNotificationsAsync([Body] CreatePushRegistrationRequestModel model);

        /// <summary>
        /// Get all push registrations
        /// </summary>
        /// <returns>All push registrations</returns>
        [Get("/api/pushRegistrations")]
        Task<List<GetPushRegistrationResponseModel>> GetAllPushNotificationRegistrationsAsync();

        /// <summary>
        /// Get all push registrations for customer
        /// </summary>
        /// <returns>All push registrations for specific customer</returns>
        [Get("/api/pushRegistrations/queryCustomer/{customerId}")]
        Task<List<GetPushRegistrationResponseModel>> GetAllPushNotificationRegistrationsForCustomerAsync(
            string customerId);

        /// <summary>
        /// Delete specific push notification registration by id
        /// </summary>
        /// <param name="pushRegistrationId">Push notification registration id</param>
        /// <returns></returns>
        [Delete("/api/pushRegistrations/{pushRegistrationId}")]
        Task DeletePushNotificationRegistration(Guid pushRegistrationId);

        /// <summary>
        /// Delete specific push notification registration by token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        [Delete("/api/pushRegistrations/token/{token}")]
        Task DeleteRegistrationByTokenAsync(string token);

        /// <summary>
        /// Delete all push notification registrations for specified customer
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <returns></returns>
        [Delete("/api/pushRegistrations/queryCustomer/{customerId}")]
        Task DeleteAllPushNotificationRegistrationsForCustomer(string customerId);

    }
}
