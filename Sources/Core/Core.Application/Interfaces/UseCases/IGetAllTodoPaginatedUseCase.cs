using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;

namespace Core.Application.Interfaces.UseCases
{
    public interface IGetAllTodoPaginatedUseCase : IUseCase<PaginationUseCaseRequest, GetAllTodoPaginatedUseCaseResponse>
    {
    }
}