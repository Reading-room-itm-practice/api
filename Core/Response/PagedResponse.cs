using System;

namespace Core.Response
{
    public class PagedResponse<T> : PagedResponseProperties
    {
        
        public T Data { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
