using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Service.PushNotifications.Client;
using MAVN.Service.PushNotifications.Client.Enums;
using MAVN.Service.PushNotifications.Client.Models.Requests;
using MAVN.Service.PushNotifications.Client.Models.Responses;
using MAVN.Service.PushNotifications.Domain;
using MAVN.Service.PushNotifications.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.PushNotifications.Controllers
{
    [Route("/api/")]
    [ApiController]
    public class PushRegistrationsController : Controller, IPushRegistrationsApi, IPushNotificationsApi
    {
        private readonly IPushNotificationRegistrationService _pushNotificationRegistrationService;
        private readonly IMapper _mapper;

        public PushRegistrationsController(IPushNotificationRegistrationService pushNotificationRegistrationService,
            IMapper mapper)
        {
            _pushNotificationRegistrationService = pushNotificationRegistrationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create new push registration
        /// </summary>
        /// <param name="model">model containing push registration info</param>
        /// <returns>Details on registration outcome</returns>
        [HttpPost("pushRegistrations")]
        [ProducesResponseType(typeof(PushTokenInsertionResult), (int) HttpStatusCode.OK)]
        public async Task<PushTokenInsertionResult> RegisterForPushNotificationsAsync(
            CreatePushRegistrationRequestModel model)
        {
            var data = _mapper.Map<PushNotificationRegistration>(model);

            var result = await _pushNotificationRegistrationService.RegisterForPushNotificationsAsync(data);

            return _mapper.Map<PushTokenInsertionResult>(result);
        }

        /// <summary>
        /// Get all push registrations
        /// </summary>
        /// <returns>All push registrations</returns>
        [HttpGet("pushRegistrations")]
        [ProducesResponseType(typeof(List<GetPushRegistrationResponseModel>), (int) HttpStatusCode.OK)]
        public async Task<List<GetPushRegistrationResponseModel>> GetAllPushNotificationRegistrationsAsync()
        {
            var result = await _pushNotificationRegistrationService.GetAllPushNotificationRegistrationsAsync();

            return _mapper.Map<List<GetPushRegistrationResponseModel>>(result);
        }

        /// <summary>
        /// Get all push registrations for customer
        /// </summary>
        /// <returns>All push registrations for specific customer</returns>
        [HttpGet("pushRegistrations/queryCustomer/{customerId}")]
        [ProducesResponseType(typeof(List<GetPushRegistrationResponseModel>), (int) HttpStatusCode.OK)]
        public async Task<List<GetPushRegistrationResponseModel>> GetAllPushNotificationRegistrationsForCustomerAsync(
            string customerId)
        {
            var result =
                await _pushNotificationRegistrationService.GetAllPushNotificationRegistrationsForCustomerAsync(
                    customerId);

            return _mapper.Map<List<GetPushRegistrationResponseModel>>(result);
        }

        /// <summary>
        /// Delete specific push notification registration by id
        /// </summary>
        /// <param name="pushRegistrationId">Push notification registration id</param>
        /// <returns></returns>
        [HttpDelete("pushRegistrations/{pushRegistrationId}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task DeletePushNotificationRegistration(Guid pushRegistrationId)
        {
            await _pushNotificationRegistrationService.DeletePushNotificationRegistrationAsync(pushRegistrationId);
        }

        /// <summary>
        /// Delete specific push notification registration by token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        [HttpDelete("pushRegistrations/token/{token}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task DeleteRegistrationByTokenAsync(string token)
        {
            await _pushNotificationRegistrationService.DeleteRegistrationByTokenAsync(token);
        }

        /// <summary>
        ///  Delete all push notification registrations for specified customer
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <returns></returns>
        [HttpDelete("pushRegistrations/queryCustomer/{customerId}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task DeleteAllPushNotificationRegistrationsForCustomer(string customerId)
        {
            await _pushNotificationRegistrationService.DeleteAllPushNotificationRegistrationsForCustomerAsync(
                customerId);
        }
    }
}
