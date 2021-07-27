using System.Collections.Generic;

namespace Core.DTOs
{
    public class DataDto<T>
    {
        public IEnumerable<T> data { get; set; }
        public T singleData { get; set; }
        public int count { get; set; }
    }
}
