using System;

namespace Core.Services
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 0;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Valid();
        }
        public void Valid()
        {
            PageNumber = PageNumber < 1 ? 1 : PageNumber;
            PageSize = Math.Abs(PageSize);
        }
    }
}
