using System.ComponentModel.DataAnnotations;

namespace Lykke.Service.PushNotifications.Client.Models.Requests
{
    /// <summary>
    /// Model that represent paginated request
    /// </summary>
    public class PaginatedRequestModel
    {
        /// <summary>
        /// The current page
        /// </summary>
        [Required]
        [Range(1, 10000)]
        public int CurrentPage { get; set; }

        /// <summary>
        /// The page size
        /// </summary>
        [Required]
        [Range(1, 500)]
        public int PageSize { get; set; }
    }
}
