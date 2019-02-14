using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formaty
{
    interface IFileHelper
    {
        string FileName { get; set; }
        bool ReadFile(List<Vehicle> list);
        bool WriteFile(List<Vehicle> list);
    }
}
