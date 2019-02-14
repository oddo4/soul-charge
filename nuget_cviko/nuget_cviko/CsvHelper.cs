using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xdHelper
{
    public class CsvHelper : IFileHelper
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string SaveFilePath { get; set; }

        public CsvHelper(string fileName)
        {
            this.FileName = fileName;
        }

        public string GetFullPath()
        {
            return SaveFilePath + FileName + ".csv";
        }

        public List<T> ReadFile<T>() where T : class
        {
            var engine = new FileHelperEngine<T>();

            try
            {
                var result = engine.ReadFile(FilePath);

                if (result.Any())
                {
                    return result.ToList();
                }

            }
            catch
            {

            }

            return null;
        }

        public List<T> ReadFile<T>(string filePath) where T : class
        {
            var engine = new FileHelperEngine<T>();

            try
            {
                var result = engine.ReadFile(filePath);

                if (result.Any())
                {
                    return result.ToList();
                }

            }
            catch
            {

            }

            return null;
        }

        public bool WriteFile<T>(List<T> list) where T : class
        {
            var engine = new FileHelperEngine<object>();

            try
            {
                engine.WriteFile(GetFullPath(), list);
                return true;
            }
            catch
            {

            }

            return false;
        }
    }
}
