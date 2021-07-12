using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public interface IJsonKeyValueGetter
    {
        public string GetValueByKey(string jsonString, string key);
    }
}
