using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    [DelimitedRecord(",")]
    public class HighScoreData
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
