using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formaty
{
    class JsonHelper : IFileHelper
    {
        public string FileName { get; set; }
        public JsonHelper(string fileName)
        {
            this.FileName = fileName;
        }

        public bool ReadFile(List<Vehicle> list)
        {
            try
            {
                string fileString = File.ReadAllText(FileName);
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

        public bool WriteFile(List<Vehicle> list)
        {
            try
            {
                string json = JsonConvert.SerializeObject(list);
                File.WriteAllText(FileName, json);

                return true;
            }
            catch
            {

            }

            return false;
        }
    }
}
