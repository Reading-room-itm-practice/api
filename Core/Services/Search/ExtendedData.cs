using Core.Common;

namespace Core. Services.Search
{
    public class ExtendedData : IDto
    {

    }
    public class ExtendedData<T> : ExtendedData
    {
        public T Data { get; set; }
        public int Quantity { get; set; }
    }
}
