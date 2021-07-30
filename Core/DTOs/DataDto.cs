using System.Collections.Generic;

namespace Core.DTOs
{
    public class DataDto
    {

    }
    public class DataDto<T> : DataDto
    {
        public IEnumerable<T> Data { get; set; }
        public T SingleData { get; set; }
        public int Quantity { get; set; }
    }
}
