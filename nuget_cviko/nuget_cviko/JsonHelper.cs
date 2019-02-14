using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xdHelper
{
    public class JsonHelper : IFileHelper
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string SaveFilePath { get; set; }

        public JsonHelper(string fileName)
        {
            this.FileName = fileName;
        }

        public string GetFullPath()
        {
            return SaveFilePath + FileName + ".json";
        }

        public List<T> ReadFile<T>() where T : class
        {
            try
            {
                string fileString = File.ReadAllText(FilePath);
                var result = JsonConvert.DeserializeObject<List<T>>(fileString);

                return result.ToList();
            }
            catch
            {

            }
            return null;
        }

        public List<T> ReadFile<T>(string filePath) where T : class
        {
            try
            {
                string fileString = File.ReadAllText(filePath);
                var result = JsonConvert.DeserializeObject<List<T>>(fileString);

                return result.ToList();
            }
            catch
            {

            }
            return null;
        }

        public bool WriteFile<T>(List<T> list) where T : class
        {
            try
            {
                string json = JsonConvert.SerializeObject(list);
                File.WriteAllText(GetFullPath(), json);

                return true;
            }
            catch
            {

            }

            return false;
        }
    }
}
