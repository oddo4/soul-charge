using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chuck_norris
{
    public class JsonHelper
    {
        private string fileName;
        private string extension = ".json";

        public JsonHelper(string fileName)
        {
            this.fileName = fileName;
        }

        public ObservableCollection<Joke> ReadFile()
        {
            ObservableCollection<Joke> col = new ObservableCollection<Joke>();
            try
            {
                string fileString = File.ReadAllText(fileName + extension);
                var result = JsonConvert.DeserializeObject<List<Joke>>(fileString);

                foreach (Joke data in result)
                {
                    col.Add(data);
                }

                return col;
            }
            catch
            {
                return null;
            }
        }

        public bool WriteFile(ObservableCollection<Joke> col)
        {
            try
            {
                string json = JsonConvert.SerializeObject(col);
                File.WriteAllText(fileName + extension, json);

                return true;
            }
            catch
            {
                Debug.WriteLine("MUDA!");
            }

            return false;
        }
    }
}
