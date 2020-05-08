using System;
using System.Threading.Tasks;
using AutoFixture;
using MAVN.Common.Encryption;
using Lykke.Logs;
using MAVN.Service.PushNotifications.Domain.Contracts;
using MAVN.Service.PushNotifications.Domain.Repositories;
using MAVN.Service.PushNotifications.DomainServices;
using Moq;
using Xunit;

namespace MAVN.Service.PushNotifications.Tests
{
    public class NotificationMessagesServiceTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly NotificationMessageService _service;

        private readonly Mock<INotificationMessageRepository>
            _notificationMessageRepositoryMock = new Mock<INotificationMessageRepository>();

        private readonly Mock<IEncryptionService>
            _encryptionServiceMock = new Mock<IEncryptionService>();

        public NotificationMessagesServiceTests()
        {
            _service = new NotificationMessageService(_notificationMessageRepositoryMock.Object,
                EmptyLogFactory.Instance, _encryptionServiceMock.Object);
        }

        [Fact]
        public async Task When_InvalidCurrentPageForNotificationMessages_Expect_ExceptionThrown()
        {
            var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.GetNotificationMessagesForCustomerAsync("custId", 0, 10));

            Assert.Equal("currentPage", ex.ParamName);
        }

        [Fact]
        public async Task When_SmallPageSizeForNotificationMessages_Expect_ExceptionThrown()
        {
            var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.GetNotificationMessagesForCustomerAsync("custId", 1, 0));

            Assert.Equal("pageSize", ex.ParamName);
        }

        [Fact]
        public async Task When_LargePageSizeForNotificationMessages_Expect_ExceptionThrown()
        {
            var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.GetNotificationMessagesForCustomerAsync("custId", 1, 501));

            Assert.Equal("pageSize", ex.ParamName);
        }

        [Fact]
        public async Task When_ValidFilterForNotificationMessages_Expect_RepositoryCalled()
        {
            //Arrange
            const int currentPage = 1;
            const int pageSize = 10;
            const string custId = "custId";

            const int skip = (currentPage - 1) * pageSize;
            const int take = pageSize;

            _notificationMessageRepositoryMock.Setup(x => x.GetNotificationMessagesForCustomerAsync(skip,
                    take, custId))
                .Returns(Task.FromResult(_fixture.Create<PaginatedList<NotificationMessage>>()));

            //Act
            await _service.GetNotificationMessagesForCustomerAsync(custId, currentPage, pageSize);

            //Assert
            _notificationMessageRepositoryMock.Verify(x => x.GetNotificationMessagesForCustomerAsync(skip,
                take, custId), Times.Once);
        }

        [Fact]
        public async Task When_MessageExists_Expect_RepoNotCalled()
        {
            //Arrange
            const string messageGroupId = "messageGroupId";
            const string customerId = "customerId";

            _notificationMessageRepositoryMock.Setup(x => x.CheckIfMessageExistsAsync(messageGroupId, customerId))
                .Returns(Task.FromResult(true));

            //Act
            await _service.ProcessNotificationMessageAsync(
                Guid.NewGuid(),
                "pushId", 
                messageGroupId,
                customerId,
                "message",
                "customPayload",
                null);

            //Assert
            _notificationMessageRepositoryMock.Verify(
                x => x.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), 
                    It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
        }

        [Fact]
        public async Task When_MessageDoesNotExist_Expect_RepoCalled()
        {
            //Arrange
            const string messageGroupId = "messageGroupId";
            const string customerId = "customerId";
            const string message = "message";
            const string customPayload = "{}";
            const string encryptedMessage = "Well, I can't tell you. It's encrypted";

            _notificationMessageRepositoryMock.Setup(x => x.CheckIfMessageExistsAsync(messageGroupId, customerId))
                .Returns(Task.FromResult(false));
            _encryptionServiceMock.Setup(x => x.EncryptValue(message))
                .Returns(encryptedMessage);

            //Act
            await _service.ProcessNotificationMessageAsync(
                Guid.NewGuid(),
                "pushId",
                messageGroupId,
                customerId,
                message,
                customPayload,
                null);

            //Assert
            _notificationMessageRepositoryMock.Verify(
                x => x.CreateAsync(customerId, messageGroupId, customPayload, encryptedMessage), Times.Once);
        }
    }
}
