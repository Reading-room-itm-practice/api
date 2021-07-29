using System.Collections.Generic;

namespace Core.DTOs
{
    public class DataDto
    {

    }
    public class DataDto<T> : DataDto
    {
        public IEnumerable<T> data { get; set; }
        public T singleData { get; set; }
        public int count { get; set; }
    }
}
