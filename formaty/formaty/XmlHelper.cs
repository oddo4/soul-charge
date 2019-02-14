using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace formaty
{
    class XmlHelper : IFileHelper
    {
        public string FileName { get; set; }

        public XmlHelper(string fileName)
        {
            this.FileName = fileName;
        }

        public bool ReadFile(List<Vehicle> list)
        {
            try
            {
                XmlSerializer xmlDoc = new XmlSerializer(list.GetType());
                list = (List<Vehicle>)xmlDoc.Deserialize(new StreamReader(FileName));

                return true;
            }
            catch
            {

            }

            return false;
        }

        public bool WriteFile(List<Vehicle> list)
        {
            try
            {
                XmlSerializer xmlDoc = new XmlSerializer(list.GetType());
                xmlDoc.Serialize(new StreamWriter(FileName), list);

                return true;
            }
            catch
            {

            }

            return false;
        }
    }
}
