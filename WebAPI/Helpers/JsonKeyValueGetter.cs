using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Helpers
{
    public class JsonKeyValueGetter : IJsonKeyValueGetter
    {
        public string GetValueByKey(string jsonString, string key)
        {
            var json = (JObject)JsonConvert.DeserializeObject(jsonString);
            
            return json[key].Value<string>();
        }
    }
}
