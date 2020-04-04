using System.Collections.Generic;

namespace MAVN.Service.PushNotifications.Client.Models.Responses
{
    /// <summary>
    /// Paginated list with desired page and information about the page
    /// </summary>
    public class PaginatedResponseModel<T>
    {
        /// <summary>
        /// The CurrentPage
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The PageSize
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The TotalCount of audit messages
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// List of AuditMessages for the given page
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}
