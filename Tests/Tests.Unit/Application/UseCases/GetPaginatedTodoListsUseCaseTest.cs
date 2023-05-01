using AutoMapper;
using Core.Application.Dtos.Requests;
using Core.Application.Interfaces.Repositories;
using Core.Application.Mappings;
using Core.Application.UseCases;
using Core.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Unit.Application.UseCases
{
    public class GetPaginatedTodoListsUseCaseTest
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<ITodoRepositoryAsync> _todoRepositoryAsync;
        private readonly Mock<ILogger<GetPaginatedTodoListsUseCase>> _loggerMock;

        public GetPaginatedTodoListsUseCaseTest()
        {
            // Repository mock
            _todoRepositoryAsync = new Mock<ITodoRepositoryAsync>();

            // Logger mock
            _loggerMock = new Mock<ILogger<GetPaginatedTodoListsUseCase>>();

            // Set auto mapper configs
            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();
        }

        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Should execute successfully")]
        public async Task ShouldExecuteSucessfully()
        {
            // Arranje
            var todos = new List<Todo>()
            {
                new Todo(1, "Ir ao mercado.", false),
                new Todo(2, "Fazer investimentos.", true),
                new Todo(3, "Fazer atividade física.", false),
                new Todo(4, "Pagar as contas do mês.", true)
            };

            _todoRepositoryAsync.Setup(x => x.GetAllAsync()).ReturnsAsync(todos);

            var useCase = new GetPaginatedTodoListsUseCase(_todoRepositoryAsync.Object, _mapperMock, _loggerMock.Object);

            int pageNumber = 1;
            int pageSize = 10;
            int maxPageSize = 50;
            int defaultPageSize = 10;
            int initialPagination = 1;

            var useCaseRequest = new PaginationUseCaseRequest(
                pageNumber
                , pageSize
                , maxPageSize
                , defaultPageSize
                , initialPagination);

            // Act
            var useCaseResponse = await useCase.RunAsync(useCaseRequest);

            // Assert
            useCaseResponse.Should().NotBeNull();
            useCaseResponse.PageNumber.Should().Be(pageNumber);
            useCaseResponse.PageSize.Should().Be(pageSize);
            // useCaseResponse.TotalRecords.Should().Be(pageNumber);
            // useCaseResponse.TodoListUseCaseResponse.Should().BeEquivalentTo(todos);
            // useCaseResponse.TodoListUseCaseResponse.Should().HaveCount(4);

            useCaseResponse.TodoListUseCaseResponse.Should().Satisfy(
                e => e.Id == 1 && e.Title == "Ir ao mercado." && !e.Done,
                e => e.Id == 2 && e.Title == "Fazer investimentos." && e.Done,
                e => e.Id == 3 && e.Title == "Fazer atividade física." && !e.Done,
                e => e.Id == 4 && e.Title == "Pagar as contas do mês." && e.Done);

            // _loggerMock
            //     .VerifyLogger("Start useCase GetPaginatedTodoUseCase > method RunAsync.", LogLevel.Information)
            //     .VerifyLogger("Finishes successfully useCase GetPaginatedTodoUseCase > method RunAsync.", LogLevel.Information);
        }
    }
}