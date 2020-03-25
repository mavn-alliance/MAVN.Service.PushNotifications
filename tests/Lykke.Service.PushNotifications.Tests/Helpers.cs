using AutoMapper;

namespace Lykke.Service.PushNotifications.Tests
{
    public static class Helpers
    {
        public static IMapper CreateAutoMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(AutoMapperProfile)));

            return config.CreateMapper();
        }
    }
}
