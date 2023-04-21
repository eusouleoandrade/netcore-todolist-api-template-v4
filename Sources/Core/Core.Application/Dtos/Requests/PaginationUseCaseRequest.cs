
namespace Core.Application.Dtos.Requests
{
    public class PaginationUseCaseRequest
    {
        private const int maxPageSize = 50;
        private const int defaultPageSize = 10;
        private const int initialPagination = 1;

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        public PaginationUseCaseRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber <= decimal.Zero ? initialPagination : pageNumber;
            PageSize = pageSize > maxPageSize ? maxPageSize : pageSize;

            PageSize = PageSize == decimal.Zero ? defaultPageSize : PageSize;
        }
    }
}