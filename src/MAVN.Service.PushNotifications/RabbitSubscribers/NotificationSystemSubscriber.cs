using Autofac;
using Common;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.NotificationSystem.Contract.MessageContracts;
using System;
using System.Threading.Tasks;
using Common.Log;
using MAVN.Service.NotificationSystem.Contract.Enums;
using MAVN.Service.PushNotifications.Domain.Services;

namespace MAVN.Service.PushNotifications.RabbitSubscribers
{
    public class NotificationSystemSubscriber : IStartable, IStopable
    {
        private readonly RabbitMqSubscriptionSettings _settings;
        private readonly string _exchangeName = "lykke.notificationsystem.brokermessage";
        private readonly string _queueNameSuffix = "pushnotifications";
        private readonly ILogFactory _logFactory;
        private readonly INotificationMessageService _service;
        private readonly ILog _log;
        private RabbitMqSubscriber<BrokerMessage> _subscriber;

        public NotificationSystemSubscriber(
            ILogFactory logFactory,
            INotificationMessageService service,
            string connectionString)
        {
            _settings = RabbitMqSubscriptionSettings.ForSubscriber(connectionString,
                    _exchangeName,
                    _queueNameSuffix)
                .MakeDurable();

            _logFactory = logFactory;
            _service = service;
            _log = _logFactory.CreateLog(this);
        }

        public void Start()
        {
            _subscriber = new RabbitMqSubscriber<BrokerMessage>(
                    _logFactory,
                    _settings,
                    new ResilientErrorHandlingStrategy(
                        _logFactory,
                        _settings,
                        TimeSpan.FromSeconds(10),
                        next: new DeadQueueErrorHandlingStrategy(_logFactory, _settings)))
                .SetMessageDeserializer(new JsonMessageDeserializer<BrokerMessage>())
                .Subscribe(ProcessMessageAsync)
                .CreateDefaultBinding()
                .Start();
        }

        private async Task ProcessMessageAsync(BrokerMessage message)
        {
            _log.Info("Message received", process: nameof(ProcessMessageAsync), context: new
            {
                message.MessageId
            });

            if (message.Channel == Channel.PushNotification)
            {
                await _service.ProcessNotificationMessageAsync(
                    message.MessageId,
                    message.Properties["PushRegistrationId"],
                    message.Properties["MessageGroupId"],
                    message.Properties["CustomerId"],
                    message.Properties["Message"],
                    message.Properties["CustomPayload"],
                    message.MessageParameters);
            }
        }

        public void Dispose()
        {
            _subscriber?.Dispose();
        }

        public void Stop()
        {
            _subscriber?.Stop();
        }
    }
}
