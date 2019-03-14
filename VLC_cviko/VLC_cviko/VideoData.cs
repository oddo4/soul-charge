using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLC_cviko
{
    public class VideoData
    {
        public string FileName { get; set; }
        public string Path { get; set; }

        public Uri GetUriPath()
        {
            return new Uri(Path);
        }
    }
}
