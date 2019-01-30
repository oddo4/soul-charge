using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class FileHelper
    {
        private string filePath;

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public List<HighScoreData> ReadCSVFile(string filePath = "")
        {
            var engine = new FileHelperEngine<HighScoreData>();

            if (filePath == "")
            {
                filePath = FilePath;
            }

            try
            {
                var result = engine.ReadFile(filePath);

                if (result.Any())
                {
                    return result.ToList<HighScoreData>();
                }

            }
            catch
            {

            }

            return null;
        }

        public bool WriteCSVFile(List<HighScoreData> list)
        {
            var engine = new FileHelperEngine<HighScoreData>();

            try
            {
                engine.WriteFile(FilePath, list);
                return true;
            }
            catch
            {

            }

            return false;
        }
    }
}
