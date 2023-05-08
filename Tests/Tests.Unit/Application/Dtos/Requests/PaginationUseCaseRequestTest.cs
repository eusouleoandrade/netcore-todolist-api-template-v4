using Core.Application.Dtos.Requests;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Requests
{
    public class PaginationUseCaseRequestTest
    {
        private const int maxPageSize = 50;
        private const int defaultPageSize = 10;
        private const int initialPagination = 1;

        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        [Theory(DisplayName = "Should execute successfully")]
        [InlineData(1, 20)]
        [InlineData(2, 49)]
        [InlineData(3, 50)]
        public void ShouldExecuteSuccessfully(int pageNumber, int pageSize)
        {
            // Arranje
            PaginationUseCaseRequest request;

            // Act
            request = new PaginationUseCaseRequest(
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

        /// <summary>
        /// Should execute successfully when the maximum page size is larger than configured
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        [Theory(DisplayName = "Should execute successfully when the maximum page size is larger than configured")]
        [InlineData(1, 51)]
        [InlineData(2, 60)]
        [InlineData(3, 100)]
        public void ShouldExecuteSuccessfully_WhenTheMaximumPageSizeIsLargerThanConfigured(int pageNumber, int pageSize)
        {
            // Arranje
            PaginationUseCaseRequest request;

            // Act
            request = new PaginationUseCaseRequest(
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

        /// <summary>
        /// Should execute successfully when the page number is invalid
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        [Theory(DisplayName = "Should execute successfully when the page number is invalid")]
        [InlineData(0, 20)]
        [InlineData(-1, 20)]
        [InlineData(-10, 20)]
        public void ShouldExecuteSuccessfully_WhenThePageNumberIsInvalid(int pageNumber, int pageSize)
        {
            // Arranje
            PaginationUseCaseRequest request;

            // Act
            request = new PaginationUseCaseRequest(
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

        /// <summary>
        /// Should execute successfully when the pageSize is invalid
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        [Theory(DisplayName = "Should execute successfully when the pageSize is invalid")]
        [InlineData(1, -1)]
        [InlineData(2, -10)]
        [InlineData(3, 0)]
        public void ShouldExecuteSuccessfully_WhenThePageSizeIsInvalid(int pageNumber, int pageSize)
        {
            // Arranje
            PaginationUseCaseRequest request;

            // Act
            request = new PaginationUseCaseRequest(
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