using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formular.Classes
{
    public class Order
    {
        public int ID { get; set; }
        public int Person_ID { get; set; }
        public string Date
        {
            get;
            set;
        }
        public int Hidden { get; set; }

        public List<KeyValuePair<string, string>> CreateKeyValues()
        {
            var list = new List<KeyValuePair<string, string>>();

            list.Add(new KeyValuePair<string, string>("Method", "Insert"));
            list.Add(new KeyValuePair<string, string>("Option", "Order"));
            list.Add(new KeyValuePair<string, string>("Person_ID", Person_ID.ToString()));

            return list;
        }
    }
}
