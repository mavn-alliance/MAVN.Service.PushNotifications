using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Lykke.Logs;
using Lykke.Service.PushNotifications.Domain;
using Lykke.Service.PushNotifications.Domain.Repositories;
using Lykke.Service.PushNotifications.DomainServices;
using Moq;
using Xunit;

namespace Lykke.Service.PushNotifications.Tests
{
    public class PushNotificationRegistrationServiceTests
    {
        private readonly PushNotificationRegistrationService _service;

        private readonly Fixture _fixture = new Fixture();

        private readonly Mock<IPushNotificationRegistrationRepository> _pushNotificationRegistrationRepository =
            new Mock<IPushNotificationRegistrationRepository>();

        public PushNotificationRegistrationServiceTests()
        {
            _service = new PushNotificationRegistrationService(_pushNotificationRegistrationRepository.Object,
                EmptyLogFactory.Instance);
        }

        [Fact]
        public async Task
            When_Register_For_Push_Notifications_Async_Is_Called_Expect_That_Repository_Method_Is_Executed()
        {
            _pushNotificationRegistrationRepository.Setup(x => x.CreateAsync(It.IsAny<PushNotificationRegistration>()))
                .Returns(Task.FromResult(It.IsAny<Domain.Enums.PushTokenInsertionResult>()));

            await _service.RegisterForPushNotificationsAsync(_fixture.Create<PushNotificationRegistration>());

            _pushNotificationRegistrationRepository.Verify(x => x.CreateAsync(It.IsAny<PushNotificationRegistration>()),
                Times.Once);
        }

        [Fact]
        public async Task
            When_Get_All_Push_Notification_Registrations_Async_Is_Called_Expect_That_Repository_Method_Is_Executed()
        {
            _pushNotificationRegistrationRepository.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(_fixture.Build<PushNotificationRegistration>().CreateMany().ToList()));

            await _service.GetAllPushNotificationRegistrationsAsync();

            _pushNotificationRegistrationRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task
            When_Get_All_Push_Notification_Registrations_For_Customer_Async_Is_Called_Expect_That_Repository_Method_Is_Executed()
        {
            _pushNotificationRegistrationRepository.Setup(x => x.GetAllForCustomerAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(_fixture.Build<PushNotificationRegistration>().CreateMany().ToList()));

            await _service.GetAllPushNotificationRegistrationsForCustomerAsync(It.IsAny<string>());

            _pushNotificationRegistrationRepository.Verify(x => x.GetAllForCustomerAsync(It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task
            When_Delete_Push_Notification_Registration_Async_Is_Called_Expect_That_Repository_Method_Is_Executed()
        {
            _pushNotificationRegistrationRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(It.IsAny<bool>()));

            await _service.DeletePushNotificationRegistrationAsync(It.IsAny<Guid>());

            _pushNotificationRegistrationRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>()),
                Times.Once);
        }

        [Fact]
        public async Task
            When_DeleteRegistrationByTokenAsync_IsCalled_Expect_ThatRepositoryMethodIsExecuted()
        {
            var token = "someToken";

            _pushNotificationRegistrationRepository.Setup(x => x.DeleteByTokenAsync(token))
                .Returns(Task.FromResult(true));

            await _service.DeleteRegistrationByTokenAsync(token);

            _pushNotificationRegistrationRepository.Verify(x => x.DeleteByTokenAsync(token),
                Times.Once);
        }

        [Fact]
        public async Task
            When_Delete_Push_Notification_Registrations_For_Customer_Async_Is_Called_Expect_That_Repository_Method_Is_Executed()
        {
            _pushNotificationRegistrationRepository.Setup(x => x.DeleteAllForCustomerAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(It.IsAny<bool>()));

            await _service.DeleteAllPushNotificationRegistrationsForCustomerAsync(It.IsAny<string>());

            _pushNotificationRegistrationRepository.Verify(x => x.DeleteAllForCustomerAsync(It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task
            When_Register_For_Push_Notifications_Async_Is_Called_With_Invalid_Data_Expect_That_Argument_Exception_Is_Thrown()
        {
            _pushNotificationRegistrationRepository.Setup(x => x.CreateAsync(It.IsAny<PushNotificationRegistration>()))
                .Returns(Task.FromResult(It.IsAny<Domain.Enums.PushTokenInsertionResult>()));

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _service.RegisterForPushNotificationsAsync(_fixture.Build<PushNotificationRegistration>()
                    .With(x => x.CustomerId, "").Create());
            });
        }
    }
}
