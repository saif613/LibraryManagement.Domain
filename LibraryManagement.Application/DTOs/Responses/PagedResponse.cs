using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.DTOs.Responses
{
    public class PagedResponse<T>
    {
        public List<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public PagedResponse(List<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        }
    }
}
