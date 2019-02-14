using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xdHelper
{
    public interface IFileHelper
    {
        string FileName { get; set; }
        string FilePath { get; set; }
        string SaveFilePath { get; set; }
        string GetFullPath();
        List<T> ReadFile<T>() where T : class;
        bool WriteFile<T>(List<T> list) where T : class;
    }
}
