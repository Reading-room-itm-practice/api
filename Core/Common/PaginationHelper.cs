using Core.Interfaces;
using Core.Response;
using Core.Services;
using System;

namespace Core.Common
{
    public class PaginationHelper
    {

        public static PagedResponse<T> CreatePagedReponse<T>(T pagedData, PaginationFilter validFilter, int totalRecords, IUriService uriService, string route)
        {

            var respose = new PagedResponse<T>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(validFilter.PageSize == 0 ? 1 : (totalRecords / (double)validFilter.PageSize)));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
