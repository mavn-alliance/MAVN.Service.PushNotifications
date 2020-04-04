using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.PushNotifications.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string ConnectionString { get; set; }
    }
}
