using MAVN.Service.PushNotifications.Client.Enums;

namespace MAVN.Service.PushNotifications.Client.Models.Responses
{
    /// <summary>
    /// Model that represents response on create push registration request 
    /// </summary>
    public class CreatePushRegistrationResponseModel
    {
        /// <summary>
        /// Result for the request that was made
        /// </summary>
        public PushTokenInsertionResult ResultCode { get; set; }
    }
}
