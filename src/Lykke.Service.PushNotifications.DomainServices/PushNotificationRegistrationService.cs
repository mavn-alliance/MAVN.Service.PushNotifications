using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.PushNotifications.Domain;
using Lykke.Service.PushNotifications.Domain.Repositories;
using Lykke.Service.PushNotifications.Domain.Services;
using Lykke.Service.PushNotifications.DomainServices.Validation;

namespace Lykke.Service.PushNotifications.DomainServices
{
    public class PushNotificationRegistrationService : IPushNotificationRegistrationService
    {
        private readonly IPushNotificationRegistrationRepository _pushNotificationRegistrationRepository;
        private readonly ILog _log;

        public PushNotificationRegistrationService(
            IPushNotificationRegistrationRepository pushNotificationRegistrationRepository, ILogFactory logFactory)
        {
            _pushNotificationRegistrationRepository = pushNotificationRegistrationRepository;
            _log = logFactory.CreateLog(this);
        }

        public async Task<Domain.Enums.PushTokenInsertionResult> RegisterForPushNotificationsAsync(
            PushNotificationRegistration data)
        {
            var validator = new RegisterForPushNotificationsValidator();
            var validationResult = validator.Validate(data);

            if(!validationResult.IsValid)
                throw new ArgumentException(validationResult.ToString());

            return await _pushNotificationRegistrationRepository.CreateAsync(data);
        }

        public async Task<List<PushNotificationRegistration>> GetAllPushNotificationRegistrationsAsync()
        {
            return await _pushNotificationRegistrationRepository.GetAllAsync();
        }

        public async Task<List<PushNotificationRegistration>> GetAllPushNotificationRegistrationsForCustomerAsync(
            string customerId)
        {
            return await _pushNotificationRegistrationRepository.GetAllForCustomerAsync(customerId);
        }

        public async Task DeletePushNotificationRegistrationAsync(Guid pushRegistrationId)
        {
            var success = await _pushNotificationRegistrationRepository.DeleteAsync(pushRegistrationId);

            if (!success)
                _log.Info("Push Notification Registration not found for deletion", context: new {pushRegistrationId});
        }

        public async Task DeleteRegistrationByTokenAsync(string token)
        {
            var success = await _pushNotificationRegistrationRepository.DeleteByTokenAsync(token);

            if (!success)
                _log.Info("Push Notification Registration not found for deletion", context: new {token});
        }

        public async Task DeleteAllPushNotificationRegistrationsForCustomerAsync(string customerId)
        {
            var success = await _pushNotificationRegistrationRepository.DeleteAllForCustomerAsync(customerId);

            if (!success)
                _log.Info("Customer Push Notification Registrations not found for deletion", context: new {customerId});
        }
    }
}
