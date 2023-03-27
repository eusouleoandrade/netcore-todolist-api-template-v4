// using AutoMapper;
// using Core.Application.Interfaces.Repositories;
// using Core.Application.Mappings;
// using Core.Application.UseCases;
// using Core.Domain.Entities;
// using FluentAssertions;
// using Microsoft.Extensions.Logging;
// using Moq;
// using Tests.Unit.Extensions;

// namespace Tests.Unit.Application.UseCases
// {
//     public class GetAllPaginatedTodoUseCaseTest
//     {
//         private readonly IMapper _mapperMock;
//         private readonly Mock<IGenericRepositoryAsync<Todo, int>> _genericRepositoryAsyncMock;

//         private readonly Mock<ILogger<GetAllPaginatedTodoUseCase>> _loggerMock;

//         public GetAllPaginatedTodoUseCaseTest()
//         {
//             // Repository mock
//             _genericRepositoryAsyncMock = new Mock<IGenericRepositoryAsync<Todo, int>>();

//             // Logger mock
//             _loggerMock = new Mock<ILogger<GetAllPaginatedTodoUseCase>>();

//             // Set auto mapper configs
//             var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
//             _mapperMock = mapperConfigurationMock.CreateMapper();
//         }

//         /// <summary>
//         /// Should execute successfully
//         /// </summary>
//         /// <returns></returns>
//         [Fact(DisplayName = "Should execute successfully")]
//         public async Task ShouldExecuteSucessfully()
//         {
//             // Arranje
//             var todos = new List<Todo>()
//             {
//                 new Todo(1, "Ir ao mercado.", false),
//                 new Todo(2, "Fazer investimentos.", true),
//                 new Todo(3, "Fazer atividade física.", false),
//                 new Todo(4, "Pagar as contas do mês.", true)
//             };

//             _genericRepositoryAsyncMock.Setup(x => x.GetAllAsync()).ReturnsAsync(todos);
//             var getAllPaginatedTodoUseCase = new GetAllPaginatedTodoUseCase(_genericRepositoryAsyncMock.Object, _mapperMock, _loggerMock.Object);

//             // Act
//             var todoQueryList = await getAllPaginatedTodoUseCase.RunAsync();

//             // Assert
//             todoQueryList.Should().NotBeNullOrEmpty();
//             todoQueryList.Should().BeEquivalentTo(todos);
//             todoQueryList.Should().HaveCount(4);

//             todoQueryList.Should().Satisfy(
//                 e => e.Id == 1 && e.Title == "Ir ao mercado." && !e.Done,
//                 e => e.Id == 2 && e.Title == "Fazer investimentos." && e.Done,
//                 e => e.Id == 3 && e.Title == "Fazer atividade física." && !e.Done,
//                 e => e.Id == 4 && e.Title == "Pagar as contas do mês." && e.Done);

//             _loggerMock
//                 .VerifyLogger("Start useCase GetAllPaginatedTodoUseCase > method RunAsync.", LogLevel.Information)
//                 .VerifyLogger("Finishes successfully useCase GetAllPaginatedTodoUseCase > method RunAsync.", LogLevel.Information);
//         }
//     }
// }