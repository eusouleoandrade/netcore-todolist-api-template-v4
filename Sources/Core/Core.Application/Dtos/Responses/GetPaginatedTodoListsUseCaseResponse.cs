
namespace Core.Application.Dtos.Responses
{
    public class GetPaginatedTodoListsUseCaseResponse
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalRecords { get; private set; }
        public IReadOnlyList<TodoUseCaseResponse> TodoListUseCaseResponse { get; private set; }

        public GetPaginatedTodoListsUseCaseResponse(
            int pageNumber
            , int pageSize
            , int totalPages
            , int totalRecords
            , IReadOnlyList<TodoUseCaseResponse> todosUseCaseResponse)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
            TodoListUseCaseResponse = todosUseCaseResponse;
        }
    }
}