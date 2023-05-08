using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Core.Application.Dtos.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class PagedResponse<TData> : Response<TData>
        where TData : class
    {
        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("page_records")]
        public int TotalRecords { get; set; }

        public PagedResponse(TData data, int pageNumber, int pageSize, int totalPages, int totalRecords)
            : base(data, succeeded: true, message: null, errors: null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }
    }
}