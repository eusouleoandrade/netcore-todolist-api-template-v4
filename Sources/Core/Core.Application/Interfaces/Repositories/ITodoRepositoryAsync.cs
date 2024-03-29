using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface ITodoRepositoryAsync : IGenericRepositoryAsync<Todo, int>
    {
        Task<Todo?> CreateAsync(Todo entity);

        Task<IEnumerable<Todo>> GetPaginatedTodoListsAsync(int pageSize, int pageNumber);

        Task<int> GetTotalRecordsAsync();
    }
}