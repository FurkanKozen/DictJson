using System.Collections.Generic;

namespace DictJson
{
    public class JsonObject : Dictionary<string, object>
    {
        public JsonObject _(string key, object value)
        {
            Add(key, value);
            return this;
        }
    }
}