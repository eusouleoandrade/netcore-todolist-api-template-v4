
namespace Core.Application.Dtos.Responses
{
    public class GetAllPaginatedTodoUseCaseResponse
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public IReadOnlyList<TodoUseCaseResponse> TodosUseCaseResponse { get; private set; }

        public GetAllPaginatedTodoUseCaseResponse(int pageNumber, int pageSize, IReadOnlyList<TodoUseCaseResponse> todosUseCaseResponse)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TodosUseCaseResponse = todosUseCaseResponse;
        }
    }
}