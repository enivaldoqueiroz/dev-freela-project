namespace DevFreela.Core.Models
{
    public class PaginationResult<T>
    {
        public PaginationResult() { }

        public PaginationResult(int page, int totalPages, int pageSize, int itensCount, List<T> data)
        {
            Page = page;
            TotalPages = totalPages;
            PageSize = pageSize;
            ItensCount = itensCount;
            Data = data;
        }

        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int ItensCount { get; set; }
        public List<T> Data { get; set; }
    }
}
