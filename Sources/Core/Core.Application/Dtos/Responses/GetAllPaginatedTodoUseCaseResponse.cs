
namespace Core.Application.Dtos.Responses
{
    public class GetAllPaginatedTodoUseCaseResponse
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public int TotalRecords { get; set; }

        public IReadOnlyList<TodoUseCaseResponse> TodosUseCaseResponse { get; private set; }

        public GetAllPaginatedTodoUseCaseResponse(int pageNumber, int pageSize, int totalRecords, IReadOnlyList<TodoUseCaseResponse> todosUseCaseResponse)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TodosUseCaseResponse = todosUseCaseResponse;
        }
    }
}