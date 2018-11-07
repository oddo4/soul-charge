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
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string IDNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string DateString
        {
            get
            {
                return DateOfBirth.ToString("dd'. 'MM'. 'yyyy");
            }
        }

        public Gender Gender { get; set; }

        public List<KeyValuePair<string, string>> CreateKeyValues()
        {
            var list = new List<KeyValuePair<string, string>>();

            list.Add(new KeyValuePair<string, string>("Firstname", Firstname));
            list.Add(new KeyValuePair<string, string>("Surname", Surname));
            list.Add(new KeyValuePair<string, string>("IDNumber", IDNumber));
            list.Add(new KeyValuePair<string, string>("DateOfBirth", DateOfBirth.ToString("yyyy'-'MM'-'dd")));
            list.Add(new KeyValuePair<string, string>("Gender", Gender.ToString()));

            return list;
        }
    }
}
