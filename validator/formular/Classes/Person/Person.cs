using formular.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formular.Classes
{
    class Person
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string DateString
        {
            get
            {
                return DateOfBirth.ToString("dd'. 'MM'. 'yyyy");
            }
        }

        public List<KeyValuePair<string, string>> CreateKeyValues()
        {
            var list = new List<KeyValuePair<string, string>>();

            list.Add(new KeyValuePair<string, string>("Firstname", Firstname));
            list.Add(new KeyValuePair<string, string>("Surname", Surname));
            list.Add(new KeyValuePair<string, string>("PhoneNumber", PhoneNumber));
            list.Add(new KeyValuePair<string, string>("Email", Email));
            list.Add(new KeyValuePair<string, string>("DateOfBirth", DateOfBirth.ToString("yyyy'-'MM'-'dd")));

            return list;
        }
    }
}
