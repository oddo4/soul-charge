using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_181107
{
    class Node
    {
        public int Value { get; set; }
        private Node pointer;

        public Node Pointer
        {
            get { return pointer; }
            set { pointer = value; }
        }
    }
}
