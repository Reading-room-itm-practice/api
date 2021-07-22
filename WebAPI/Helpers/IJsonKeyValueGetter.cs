namespace WebAPI.Helpers
{
    public interface IJsonKeyValueGetter
    {
        public string GetValueByKey(string jsonString, string key);
    }
}
