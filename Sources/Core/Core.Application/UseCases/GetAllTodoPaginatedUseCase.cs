using AutoMapper;
using Core.Application.Dtos.Responses;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Microsoft.Extensions.Logging;
using Core.Application.Dtos.Requests;

namespace Core.Application.UseCases
{
    public class GetAllTodoPaginatedUseCase : IGetAllTodoPaginatedUseCase
    {
        private readonly ITodoRepositoryAsync _todoRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllTodoPaginatedUseCase> _logger;

        public GetAllTodoPaginatedUseCase(ITodoRepositoryAsync todoRepositoryAsync, IMapper mapper, ILogger<GetAllTodoPaginatedUseCase> logger)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetAllTodoPaginatedUseCaseResponse> RunAsync(PaginationUseCaseRequest request)
        {
            _logger.LogInformation(message: "Start useCase {0} > method {1}.", nameof(GetAllTodoPaginatedUseCase), nameof(RunAsync));

            var entities = await _todoRepositoryAsync.GetAllPaginatedAsync(request.PageSize, request.PageNumber);
            
            int totalRecords = await _todoRepositoryAsync.GetTotalRecordsAsync();

            int totalPages = CalculateTotalPages(totalRecords, request.PageSize);

            _logger.LogInformation("Finishes successfully useCase {0} > method {1}.", nameof(GetAllTodoPaginatedUseCase), nameof(RunAsync));

            return new GetAllTodoPaginatedUseCaseResponse(
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