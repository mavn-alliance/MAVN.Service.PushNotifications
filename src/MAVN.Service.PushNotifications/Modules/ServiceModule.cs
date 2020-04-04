using Autofac;
using JetBrains.Annotations;
using MAVN.Service.PushNotifications.DomainServices;
using MAVN.Service.PushNotifications.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.PushNotifications.Modules
{
    [UsedImplicitly]
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());

            builder.RegisterModule(new MsSqlRepositories.AutofacModule(
                _appSettings.CurrentValue.PushNotificationsService.Db.DataConnString));

            builder.RegisterType<RabbitSubscribers.NotificationSystemSubscriber>()
                .As<IStartable>()
                .SingleInstance()
                .WithParameter("connectionString", _appSettings.CurrentValue.Rabbit.ConnectionString);
        }
    }
}
