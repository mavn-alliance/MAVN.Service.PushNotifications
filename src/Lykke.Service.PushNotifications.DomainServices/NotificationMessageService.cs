using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Falcon.Common.Encryption;
using Lykke.Common.Log;
using Lykke.Service.PushNotifications.Domain.Contracts;
using Lykke.Service.PushNotifications.Domain.Repositories;
using Lykke.Service.PushNotifications.Domain.Services;

namespace Lykke.Service.PushNotifications.DomainServices
{
    public class NotificationMessageService : INotificationMessageService
    {
        private readonly INotificationMessageRepository _notificationMessageRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly ILog _log;

        public NotificationMessageService(INotificationMessageRepository notificationMessageRepository, 
            ILogFactory logFactory, IEncryptionService encryptionService)
        {
            _notificationMessageRepository = notificationMessageRepository;
            _encryptionService = encryptionService;
            _log = logFactory.CreateLog(this);
        }

        public async Task ProcessNotificationMessageAsync(
            Guid messageId,
            string pushRegistrationId,
            string messageGroupId,
            string customerId,
            string message,
            string customPayload,
            Dictionary<string, string> messageParameters)
        {
            if (await _notificationMessageRepository.CheckIfMessageExistsAsync(messageGroupId, customerId))
            {
                _log.Info($"Message with group id {messageGroupId} already saved. Skipping saving.", new
                {
                    messageId,
                    messageGroupId,
                    pushRegistrationId,
                    customerId
                });

                return;
            }

            _log.Info($"Saving message with id {messageId} to database.", new
            {
                messageId,
                messageGroupId,
                pushRegistrationId,
                customerId
            });

            var encryptedMessage = _encryptionService.EncryptValue(message);

            Dictionary<string, string> customPayloadDict = null;
            if (!string.IsNullOrWhiteSpace(customPayload))
                customPayloadDict = customPayload.DeserializeJson<Dictionary<string, string>>();

            if (messageParameters != null && messageParameters.Count > 0)
            {
                if (customPayloadDict == null)
                    customPayloadDict = new Dictionary<string, string>();

                foreach (var messageParameter in messageParameters)
                {
                    if (customPayloadDict.ContainsKey(messageParameter.Key))
                        _log.Warning(
                            $"Message parameter with key {messageParameter.Key} already exists in CustomPayload",
                            messageParameter.Key);
                    else
                        customPayloadDict.Add(messageParameter.Key, messageParameter.Value);
                }
            }

            await _notificationMessageRepository.CreateAsync(
                customerId,
                messageGroupId,
                customPayloadDict?.ToJson(),
                encryptedMessage);
        }

        public async Task<PaginatedList<NotificationMessage>> GetNotificationMessagesForCustomerAsync(string customerId, int currentPage, int pageSize)
        {
            ValidateCurrentPageAndPageSize(currentPage, pageSize);

            var skip = (currentPage - 1) * pageSize;
            var take = pageSize;

            var result = await _notificationMessageRepository.GetNotificationMessagesForCustomerAsync(skip, take,
                customerId);

            //Decrypt the messages
            foreach (var message in result.Data)
                message.Message = _encryptionService.DecryptValue(message.Message);

            result.CurrentPage = currentPage;
            result.PageSize = pageSize;

            return result;
        }

        public Task MarkMessageAsReadAsync(string messageGroupId)
        {
            _log.Info($"Marking message with group id {messageGroupId} as read",
                new { messageGroupId });

            return _notificationMessageRepository.MarkMessageAsReadAsync(messageGroupId);
        }

        public Task MarkAllMessagesAsReadAsync(string customerId)
        {
            _log.Info($"Marking all messages for customer with id {customerId} as read",
                new { customerId });

            return _notificationMessageRepository.MarkAllMessagesAsReadAsync(customerId);
        }

        public Task<int> GetUnreadMessagesCountAsync(string customerId)
        {
            return _notificationMessageRepository.GetUnreadMessagesCountAsync(customerId);
        }

        private void ValidateCurrentPageAndPageSize(int currentPage, int pageSize)
        {
            if (currentPage < 1)
            {
                throw new ArgumentException("Current page can't be negative or zero", nameof(currentPage));
            }

            if (currentPage > 10000)
            {
                throw new ArgumentException("Current page can't be above 10000", nameof(currentPage));
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Page size can't be bellow 1", nameof(pageSize));
            }

            if (pageSize > 500)
            {
                throw new ArgumentException("Page size can't be above 500", nameof(pageSize));
            }
        }
    }
}
