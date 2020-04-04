using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Service.PushNotifications.Client;
using MAVN.Service.PushNotifications.Client.Models.Requests;
using MAVN.Service.PushNotifications.Client.Models.Responses;
using MAVN.Service.PushNotifications.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.PushNotifications.Controllers
{
    [Route("/api/notificationMessages/")]
    [ApiController]
    public class NotificationMessagesController : Controller, INotificationMessagesApi
    {
        private readonly INotificationMessageService _service;
        private readonly IMapper _mapper;

        public NotificationMessagesController(INotificationMessageService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets notification messages for customer
        /// </summary>
        /// <param name="model">model containing push registration info</param>
        /// <returns>Details on registration outcome</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponseModel<NotificationMessageResponseModel>), (int)HttpStatusCode.OK)]
        public async Task<PaginatedResponseModel<NotificationMessageResponseModel>> GetNotificationMessagesForCustomerAsync(
            [FromQuery] NotificationMessagesRequestModel model)
        {
            var result = await _service.GetNotificationMessagesForCustomerAsync(model.CustomerId, model.CurrentPage,
                    model.PageSize);

            return _mapper.Map<PaginatedResponseModel<NotificationMessageResponseModel>>(result);
        }

        /// <summary>
        /// Marks a notification message as read
        /// </summary>
        /// <param name="model">Information about the message</param>
        [HttpPost("read")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task MarkMessageAsReadAsync([FromBody]MarkMessageAsReadRequestModel model)
        {
            await _service.MarkMessageAsReadAsync(model.MessageGroupId.ToString());
        }

        /// <summary>
        /// Marks all notification messages as read
        /// </summary>
        /// <param name="model">Model containing information for marking as read</param>
        [HttpPost("read/all")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task MarkAllMessagesAsReadAsync([FromBody]MarkAllMessagesAsReadRequestModel model)
        {
            await _service.MarkAllMessagesAsReadAsync(model.CustomerId);
        }

        /// <summary>
        /// Gets the count of all unread messages
        /// </summary>
        /// <param name="customerId">The customer's id</param>
        /// <returns>Count</returns>
        [HttpGet("unread/count")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<int> GetUnreadMessagesCountAsync(string customerId)
        {
            return await _service.GetUnreadMessagesCountAsync(customerId);
        }
    }
}
