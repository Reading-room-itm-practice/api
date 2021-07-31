using Core.Common;
using System.Collections.Generic;

namespace Core.DTOs
{
    public class DataDto : IDto
    {

    }
    public class DataDto<T> : DataDto
    {
        public T Data { get; set; }
        public int Quantity { get; set; }
    }
}
