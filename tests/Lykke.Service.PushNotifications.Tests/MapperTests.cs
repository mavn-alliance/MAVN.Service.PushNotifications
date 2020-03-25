using Xunit;

namespace Lykke.Service.PushNotifications.Tests
{
    public class MapperTests
    {
        [Fact]
        public void Configuration()
        {
            Helpers.CreateAutoMapper().ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
