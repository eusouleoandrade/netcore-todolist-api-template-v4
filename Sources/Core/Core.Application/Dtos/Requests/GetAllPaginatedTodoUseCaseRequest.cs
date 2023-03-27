
namespace Core.Application.Dtos.Requests
{
    public class GetAllPaginatedTodoUseCaseRequest
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public GetAllPaginatedTodoUseCaseRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}