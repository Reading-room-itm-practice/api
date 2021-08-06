
namespace Core. Services.Search
{
    public class ExtendedData
    {

    }
    public class ExtendedData<T> : ExtendedData
    {
        public T Data { get; set; }
        public int Quantity { get; set; }
    }
}
