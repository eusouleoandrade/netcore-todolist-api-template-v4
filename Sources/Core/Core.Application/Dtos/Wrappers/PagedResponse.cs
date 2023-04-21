namespace Core.Application.Dtos.Wrappers
{
    public class PagedResponse<TData> : Response<TData>
        where TData : class
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public PagedResponse(TData data, int pageNumber, int pageSize, int totalPages, int totalRecords)
            : base(data, succeeded: true, message: null, errors: null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }
    }
}