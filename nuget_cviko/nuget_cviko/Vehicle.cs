using FileHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xdHelper
{

    [DelimitedRecord(",")]
    public class Vehicle
    {
        public string Name;
        public string Brand;
        public string LicensePlate;
    }
}
