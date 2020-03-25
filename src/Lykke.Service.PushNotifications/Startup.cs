using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.Service.PushNotifications.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;
using Lykke.Common.ApiLibrary.Filters;

namespace Lykke.Service.PushNotifications
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "PushNotifications API",
            ApiVersion = "v1"
        };

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.Extend = (serviceCollection, settings) =>
                {
                    serviceCollection.AddAutoMapper(new Type[]
                    {
                        typeof(AutoMapperProfile),
                        typeof(MsSqlRepositories.AutoMapperProfile)
                    });
                };

                options.ConfigureMvcOptions = b => b.Filters.Add(typeof(NoContentFilter));

                options.SwaggerOptions = _swaggerOptions;

                options.Logs = logs =>
                {
                    logs.AzureTableName = "PushNotificationsLog";
                    logs.AzureTableConnectionStringResolver = settings => settings.PushNotificationsService.Db.LogsConnString;
                };
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IMapper mapper)
        {
            app.UseLykkeConfiguration(options =>
            {
                options.SwaggerOptions = _swaggerOptions;
            });

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
