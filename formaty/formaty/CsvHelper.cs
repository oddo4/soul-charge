using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formaty
{
    class CsvHelper : IFileHelper
    {
        public string FileName { get; set; }

        public CsvHelper(string fileName)
        {
            this.FileName = fileName;
        }

        public bool ReadFile(List<Vehicle> list)
        {
            var engine = new FileHelperEngine<Vehicle>();

            try
            {
                var result = engine.ReadFile(FileName);

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

        public bool WriteFile(List<Vehicle> list)
        {
            var engine = new FileHelperEngine<Vehicle>();

            try
            {
                engine.WriteFile(FileName, list);
                return true;
            }
            catch
            {

            }

            return false;
        }
    }
}
