namespace MAVN.Service.PushNotifications.Client.Models.Requests
{
    /// <summary>
    /// Model that represents request to create push registration
    /// </summary>
    public class CreatePushRegistrationRequestModel
    {
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
