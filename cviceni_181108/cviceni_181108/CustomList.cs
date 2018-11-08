using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_181108
{
    class CustomList
    {

        private int[] baseArray;

        public int[] BaseArray
        {
            get { return baseArray; }
            set { baseArray = value; }
        }

        private int currentIndex = 0;

        public CustomList()
        {
            BaseArray = new int[1];
        }

        public void Add(int value)
        {
            if (currentIndex >= BaseArray.Length)
            {
                expandArray();
            }
            baseArray[currentIndex] = value;
            currentIndex++;
        }

        private void expandArray()
        {
            var newArray = new int[BaseArray.Length * 2];

            for (int i = 0; i < BaseArray.Length; i++)
            {
                newArray[i] = BaseArray[i];
            }

            BaseArray = newArray;
        }

        public void Remove()
        {

        }

        public void Print()
        {
            for (int i = 0; i < BaseArray.Length; i++)
            {
                Debug.WriteLine(BaseArray[i]);
            }
            Debug.WriteLine("Length: " + BaseArray.Length);
        }
    }
}
