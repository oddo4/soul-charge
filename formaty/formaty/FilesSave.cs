using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;

namespace formaty
{
    class FilesSave
    {
        public string fileName;

        public FilesSave()
        {

        }

        public FilesSave(string FileName)
        {
            this.fileName = FileName;
        }

        public bool ReadCSVFile(List<Vehicle> list)
        {
            var engine = new FileHelperEngine<Vehicle>();

            try
            {
                var result = engine.ReadFile(fileName);

                if (result.Any())
                {
                    foreach (Vehicle data in result)
                    {
                        list.Add(data);
                    }

                    return true;
                }
                
            }
            catch
            {
                
            }

            return false;
        }
        public bool WriteCSVFile(List<Vehicle> list)
        {
            var engine = new FileHelperEngine<Vehicle>();

            try
            {
                engine.WriteFile(fileName, list);
                return true;
            }
            catch
            {

            }

            return false;
        }

        public bool ReadJSFile(List<Vehicle> list)
        {
            try
            {
                string fileString = File.ReadAllText(fileName);
                var result = JsonConvert.DeserializeObject<List<Vehicle>>(fileString);

                foreach (Vehicle data in result)
                {
                    list.Add(data);
                }

                return true;
            }
            catch
            {

            }
            return false;
        }

        public bool WriteJSFile(List<Vehicle> list)
        {
            try
            {
                string json = JsonConvert.SerializeObject(list);
                File.WriteAllText(fileName + ".json", json);

                return true;
            }
            catch
            {

            }

            return false;
        }

        public bool ReadXMLFile(List<Vehicle> list)
        {
            try
            {
                XmlSerializer xmlDoc = new XmlSerializer(list.GetType());
                list = (List<Vehicle>)xmlDoc.Deserialize(new StreamReader(fileName));

                return true;
            }
            catch
            {

            }

            return false;
        }

        public bool WriteXMLFile(List<Vehicle> list)
        {
            try
            {
                XmlSerializer xmlDoc = new XmlSerializer(list.GetType());
                xmlDoc.Serialize(new StreamWriter(fileName + ".xml"), list);

                return true;
            }
            catch
            {

            }

            return false;
        }
    }
}
