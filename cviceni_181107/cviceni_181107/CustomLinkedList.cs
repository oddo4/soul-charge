using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_181107
{
    class CustomLinkedList
    {
        private Node head;

        public Node Head
        {
            get { return head; }
            set { head = value; }
        }


        public void Print()
        {
            var node = Head;
            while (node != null)
            {
                Debug.WriteLine(node.Value);
                node = node.Pointer;
            }
        }

        public void AddLast(Node newNode)
        {
            Node lastNode = null;
            var node = Head;
            
            while (node != null)
            {
                lastNode = node;
                node = node.Pointer;
            }

            lastNode.Pointer = newNode;
        }

        public void AddFirst(int Value)
        {
            var lastHead = Head;
            Head = new Node() { Value = Value };
            Head.Pointer = lastHead;
        }
    }
}
