using System.Collections.Generic;

namespace MAVN.Service.PushNotifications.Domain.Contracts
{
    public class PaginatedList<T>
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
