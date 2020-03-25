using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.PushNotifications.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string ConnectionString { get; set; }
    }
}
