using Core.DTOs;
using System;
using System.Collections.Generic;

namespace Core.Response
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public PagedResponse(DataDto<T> data, int pageNumber, int pageSize)
        {
            Data = data.data;
            TotalRecords = data.count;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
