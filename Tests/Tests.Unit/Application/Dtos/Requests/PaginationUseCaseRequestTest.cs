using Core.Application.Dtos.Requests;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Requests
{
    public class PaginationUseCaseRequestTest
    {
        private const int maxPageSize = 50;
        private const int defaultPageSize = 10;
        private const int initialPagination = 1;

        [Theory(DisplayName = "Should execute successfully")]
        [InlineData(1, 20)]
        [InlineData(2, 49)]
        [InlineData(3, 50)]
        public void ShouldExecuteSuccessfully(int pageNumber, int pageSize)
        {
            // Act
            var request = new PaginationUseCaseRequest(
                pageNumber
                , pageSize
                , maxPageSize
                , defaultPageSize
                , initialPagination);

            // Assert
            request.Should().NotBeNull();
            request.PageNumber.Should().Be(pageNumber);
            request.PageSize.Should().Be(pageSize);
        }

        [Theory(DisplayName = "Should execute successfully")]
        [InlineData(1, 51)]
        [InlineData(2, 60)]
        [InlineData(3, 100)]
        public void ShouldExecuteSuccessfully_WhenTheMaximumPageSizeIsLargerThanConfigured(int pageNumber, int pageSize)
        {
            // Act
            var request = new PaginationUseCaseRequest(
                pageNumber
                , pageSize
                , maxPageSize
                , defaultPageSize
                , initialPagination);

            // Assert
            request.Should().NotBeNull();
            request.PageNumber.Should().Be(pageNumber);
            request.PageSize.Should().Be(maxPageSize);
            request.PageSize.Should().NotBe(pageSize);
        }

        [Theory]
        [InlineData(0, 20)]
        [InlineData(-1, 20)]
        [InlineData(-10, 20)]
        public void ShouldExecuteSuccessfully_WhenThePageNumberIsInvalid(int pageNumber, int pageSize)
        {
            // Act
            var request = new PaginationUseCaseRequest(
                pageNumber
                , pageSize
                , maxPageSize
                , defaultPageSize
                , initialPagination);

            // Assert
            request.Should().NotBeNull();
            request.PageNumber.Should().Be(initialPagination);
            request.PageNumber.Should().NotBe(pageNumber);
            request.PageSize.Should().Be(pageSize);
        }

        [Theory]
        [InlineData(1, -1)]
        [InlineData(2, -10)]
        [InlineData(3, 0)]
        public void ShouldExecuteSuccessfully_WhenThePageSizeIsInvalid(int pageNumber, int pageSize)
        {
            // Act
            var request = new PaginationUseCaseRequest(
                pageNumber
                , pageSize
                , maxPageSize
                , defaultPageSize
                , initialPagination);

            // Assert
            request.Should().NotBeNull();
            request.PageNumber.Should().Be(pageNumber);
            request.PageSize.Should().Be(defaultPageSize);
            request.PageSize.Should().NotBe(pageSize);
        }
    }
}