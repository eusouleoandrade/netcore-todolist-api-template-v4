using AutoMapper;
using Core.Application.Dtos.Responses;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Microsoft.Extensions.Logging;
using Core.Application.Dtos.Requests;

namespace Core.Application.UseCases
{
    public class GetPaginatedTodoListsUseCase : IGetPaginatedTodoListsUseCase
    {
        private readonly ITodoRepositoryAsync _todoRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaginatedTodoListsUseCase> _logger;

        public GetPaginatedTodoListsUseCase(ITodoRepositoryAsync todoRepositoryAsync, IMapper mapper, ILogger<GetPaginatedTodoListsUseCase> logger)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetPaginatedTodoListsUseCaseResponse> RunAsync(PaginationUseCaseRequest request)
        {
            _logger.LogInformation(message: "Start useCase {0} > method {1}.", nameof(GetPaginatedTodoListsUseCase), nameof(RunAsync));

            var entities = await _todoRepositoryAsync.GetPaginatedTodoListsAsync(request.PageSize, request.PageNumber);
            
            int totalRecords = await _todoRepositoryAsync.GetTotalRecordsAsync();

            int totalPages = CalculateTotalPages(totalRecords, request.PageSize);

            _logger.LogInformation("Finishes successfully useCase {0} > method {1}.", nameof(GetPaginatedTodoListsUseCase), nameof(RunAsync));

            return new GetPaginatedTodoListsUseCaseResponse(
                request.PageNumber
                , request.PageSize
                , totalPages
                , totalRecords
                , _mapper.Map<IReadOnlyList<TodoUseCaseResponse>>(entities));
        }

        private int CalculateTotalPages(int totalRecords, int pageSize)
        {
            var totalPages = ((double)totalRecords / (double)pageSize);
            return Convert.ToInt32(Math.Ceiling(totalPages));
        }
    }
}