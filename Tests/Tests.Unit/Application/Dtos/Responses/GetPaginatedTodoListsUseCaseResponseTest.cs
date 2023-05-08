using Core.Application.Dtos.Responses;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Responses
{
    public class GetPaginatedTodoListsUseCaseResponseTest
    {
        [Fact]
        public void ShouldExecuteSuccessfully()
        {
            // Arranje
            var todoUseCaseResponseList = new List<TodoUseCaseResponse>()
            {
                new TodoUseCaseResponse(1, "Ir ao mercado.", false),
                new TodoUseCaseResponse(2, "Fazer investimentos.", true),
                new TodoUseCaseResponse(3, "Fazer atividade física.", false),
                new TodoUseCaseResponse(4, "Pagar as contas do mês.", true)
            };

            int pageNumber = 1;
            int pageSize = 10;
            int totalPages = 1;
            int totalRecords = todoUseCaseResponseList.Count();

            // Act
            var response = new GetPaginatedTodoListsUseCaseResponse(pageNumber, pageSize, totalPages, totalRecords, todoUseCaseResponseList);

            // Assert
            response.Should().NotBeNull();

            response.PageNumber.Should().Be(pageNumber);
            response.PageSize.Should().Be(pageSize);
            response.TotalPages.Should().Be(totalPages);
            response.TotalRecords.Should().Be(totalRecords);

            response.TodoListUseCaseResponse.Should().BeEquivalentTo(todoUseCaseResponseList);
            response.TodoListUseCaseResponse.Should().HaveCount(totalRecords);

            response.TodoListUseCaseResponse.Should().Satisfy(
                e => e.Id == 1 && e.Title == "Ir ao mercado." && !e.Done,
                e => e.Id == 2 && e.Title == "Fazer investimentos." && e.Done,
                e => e.Id == 3 && e.Title == "Fazer atividade física." && !e.Done,
                e => e.Id == 4 && e.Title == "Pagar as contas do mês." && e.Done);
        }
    }
}