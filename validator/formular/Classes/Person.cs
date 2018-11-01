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

        public string Gender { get; set; }

        public List<KeyValuePair<string, string>> CreateKeyValues()
        {
            var list = new List<KeyValuePair<string, string>>();

            list.Add(new KeyValuePair<string, string>("Firstname", Firstname));
            list.Add(new KeyValuePair<string, string>("Surname", Surname));
            list.Add(new KeyValuePair<string, string>("IDNumber", IDNumber));
            list.Add(new KeyValuePair<string, string>("DateOfBirth", DateOfBirth.ToString("yyyy'-'MM'-'dd")));
            list.Add(new KeyValuePair<string, string>("Gender", Gender));

            return list;
        }

        public void SetGender(int type = 0)
        {
            switch (type)
            {
                case 0:
                    Gender = "M";
                    break;
                case 1:
                    Gender = "F";
                    break;
                case 2:
                    Gender = "?";
                    break;
                default:
                    Gender = "?";
                    break;
            }
        }
    }
}
