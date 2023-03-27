using AutoMapper;
using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Core.Application.UseCases
{
    public class GetAllPaginatedTodoUseCase : IGetAllPaginatedTodoUseCase
    {
        private readonly ITodoRepositoryAsync _todoRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPaginatedTodoUseCase> _logger;
        private readonly IConfiguration _configuration;

        public GetAllPaginatedTodoUseCase(ITodoRepositoryAsync todoRepositoryAsync, IMapper mapper, ILogger<GetAllPaginatedTodoUseCase> logger, IConfiguration configuration)
        {
            _todoRepositoryAsync = todoRepositoryAsync;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<GetAllPaginatedTodoUseCaseResponse?> RunAsync(GetAllPaginatedTodoUseCaseRequest request)
        {
            _logger.LogInformation(message: "Start useCase {0} > method {1}.", nameof(GetAllPaginatedTodoUseCase), nameof(RunAsync));

            var requestProcessed = ProcessRequest(request);

            var entities = await _todoRepositoryAsync.GetAllPaginatedAsync(requestProcessed.PageSize, requestProcessed.PageNumber);

            _logger.LogInformation("Finishes successfully useCase {0} > method {1}.", nameof(GetAllPaginatedTodoUseCase), nameof(RunAsync));

            return new GetAllPaginatedTodoUseCaseResponse(
                requestProcessed.PageNumber
                , requestProcessed.PageSize
                , _mapper.Map<IReadOnlyList<TodoUseCaseResponse>>(entities));
        }

        private GetAllPaginatedTodoUseCaseRequest ProcessRequest(GetAllPaginatedTodoUseCaseRequest request)
        {
            int maxPageSize = int.Parse(_configuration.GetSection("PaginationSettings:MaxPageSize").Value);

            int pageNumber = request.PageNumber <= decimal.Zero ? 1 : request.PageNumber;
            int pageSize = request.PageSize > maxPageSize ? maxPageSize : request.PageSize;

            return new GetAllPaginatedTodoUseCaseRequest(pageNumber, pageSize);
        }
    }
}