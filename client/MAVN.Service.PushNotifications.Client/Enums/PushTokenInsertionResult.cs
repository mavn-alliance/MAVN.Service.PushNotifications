namespace MAVN.Service.PushNotifications.Client.Enums
{
    /// <summary>
    /// Enumeration that represents result of an token insertion operation
    /// </summary>
    public enum PushTokenInsertionResult
    {
        /// <summary>
        /// Ok
        /// </summary>
        Ok,

        /// <summary>
        /// Push registration token already exists
        /// </summary>
        PushRegistrationTokenAlreadyExists,
    }
}
