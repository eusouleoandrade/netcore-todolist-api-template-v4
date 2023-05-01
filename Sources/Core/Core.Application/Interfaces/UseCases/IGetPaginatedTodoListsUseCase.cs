using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;

namespace Core.Application.Interfaces.UseCases
{
    public interface IGetPaginatedTodoListsUseCase : IUseCase<PaginationUseCaseRequest, GetPaginatedTodoListsUseCaseResponse>
    {
    }
}