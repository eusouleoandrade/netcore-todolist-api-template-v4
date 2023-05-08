
namespace Core.Application.Dtos.Requests
{
    public class PaginationUseCaseRequest
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        public PaginationUseCaseRequest(
            int pageNumber
            , int pageSize
            , int maxPageSize
            , int defaultPageSize
            , int initialPagination)
        {
            PageNumber = pageNumber <= decimal.Zero ? initialPagination : pageNumber;
            
            PageSize = pageSize <= maxPageSize ? pageSize : maxPageSize;

            PageSize = PageSize <= decimal.Zero ? defaultPageSize : PageSize;
        }
    }
}