using Lib.Notification.Models;

namespace Core.Application.Dtos.Wrappers
{
    public class PagedResponse<TData> : Response<TData>
        where TData : class
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public Uri? FirstPage { get; set; }

        public Uri? LastPage { get; set; }

        public int? TotalPages { get; set; }

        public int? TotalRecords { get; set; }

        public Uri? NextPage { get; set; }

        public Uri? PreviousPage { get; set; }

        public PagedResponse(TData data, int pageNumber, int pageSize)
            : base(data, succeeded: true, message: null, errors: null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}