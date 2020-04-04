using System;
using JetBrains.Annotations;

namespace MAVN.Service.PushNotifications.Client.Models.Responses
{
    /// <summary>
    /// Model that represents response on get push registration request 
    /// </summary>
    [PublicAPI]
    public class GetPushRegistrationResponseModel
    {
        /// <summary>
        /// Push registration id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Date of registration
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Id of the customer that will receive push notifications
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Push registration token
        /// </summary>
        public string PushRegistrationToken { get; set; }
    }
}
