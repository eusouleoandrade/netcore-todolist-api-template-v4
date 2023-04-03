using System.Diagnostics.CodeAnalysis;
using Core.Application.Exceptions;
using Core.Application.Interfaces.Repositories;
using Core.Application.Resources;
using Core.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infra.Persistence.Repositories
{
    [ExcludeFromCodeCoverage]
    public class TodoRepositoryAsync : GenericRepositoryAsync<Todo, int>, ITodoRepositoryAsync
    {
        private readonly ILogger<TodoRepositoryAsync> _logger;

        public TodoRepositoryAsync(IConfiguration configuration, ILogger<TodoRepositoryAsync> logger)
            : base(configuration, logger)
        {
            _logger = logger;
        }

        public async Task<Todo?> CreateAsync(Todo entity)
        {
            try
            {
                _logger.LogInformation(message: "Start repository {0} > method {1}.", nameof(TodoRepositoryAsync), nameof(CreateAsync));

                string insertSql = @"INSERT INTO todo (title, done)
                                    VALUES(@title, @done)
                                    RETURNING id;";

                var id = await _connection.ExecuteScalarAsync<int>(insertSql,
                new
                {
                    title = entity.Title,
                    done = entity.Done
                });

                if (id > decimal.Zero)
                {
                    _logger.LogInformation("Finishes successfully repository {0} > method {1}.", nameof(TodoRepositoryAsync), nameof(CreateAsync));
                    return await base.GetAsync(id);
                }

                return await Task.FromResult<Todo?>(default);
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

        public async Task<int> GetTotalRecordsAsync()
        {
            try
            {
                _logger.LogInformation(
                    message: "Start repository {0} > method {1}.", nameof(TodoRepositoryAsync), nameof(GetTotalRecordsAsync));

                string query = @"SELECT COUNT(Id)
                                    FROM Todo";

                int totalRecords = await _connection.ExecuteScalarAsync<int>(query);

                _logger.LogInformation(
                    "Finishes successfully repository {0} > method {1}.", nameof(TodoRepositoryAsync), nameof(GetAllPaginatedAsync));

                return totalRecords;
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

        public async Task<IEnumerable<Todo>> GetAllPaginatedAsync(int pageSize, int pageNumber)
        {
            try
            {
                _logger.LogInformation(message: "Start repository {0} > method {1}.", nameof(TodoRepositoryAsync), nameof(GetAllPaginatedAsync));

                string query = @"SELECT Id, Title, Done 
                                FROM Todo 
                                ORDER BY Id DESC 
                                LIMIT @PageSize 
                                OFFSET @Offset";

                var offset = (pageNumber - 1) * pageSize;

                var entities = await _connection.QueryAsync<Todo>(query,
                new
                {
                    PageSize = pageSize,
                    Offset = offset
                });

                _logger.LogInformation("Finishes successfully repository {0} > method {1}.", nameof(TodoRepositoryAsync), nameof(GetAllPaginatedAsync));

                return entities;
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }
    }
}