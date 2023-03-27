namespace Core.Application.Dtos.Requests
{
    public class GetAllPaginatedTodoRequest
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public GetAllPaginatedTodoRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}