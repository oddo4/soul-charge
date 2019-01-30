using Newtonsoft.Json;
using System;

namespace Lib
{
    public class Class1
    {
        public int MyProperty { get; set; }

        public string Serialize(string text)
        {
            return JsonConvert.SerializeObject(text);
        }
    }
}
