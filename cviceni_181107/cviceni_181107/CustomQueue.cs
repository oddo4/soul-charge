using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_181107
{
    class CustomQueue
    {
        private Node head;

        public Node Head
        {
            get { return head; }
            set { head = value; }
        }

        public void Peek()
        {
            Node lastNode = null;
            var node = Head;

            while (node != null)
            {
                lastNode = node;
                node = node.Pointer;
            }

            Debug.WriteLine(lastNode.Value);
        }

        public void AddFirst(int Value)
        {
            var lastHead = Head;
            Head = new Node() { Value = Value };
            Head.Pointer = lastHead;
        }
    }
}
