using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Worter2
{
    class Compare
    {
        //private Words word1 { get; set; }
        //private Words word2 { get; set; }

        

        public bool isInArray(List<int[,]> list1, int[,] array)
        {
            foreach (var subArray in list1)
            {
                if (subArray == array)
                {
                    return true;
                }
            }

            return false;
        }

        // test in some method or EventHandler:

        //list.Add(array);

        //bool test = isInArray(list, array);

        //public List<Words> CompareWords(IEnumerator ListAEnum, IEnumerator ListBEnum) //CompareWords(itemtocompare,comparator);
        //{

        //    Words itemA = new Words();
        //    Words itemB = new Words();
        //    List<Words> additional = new List<Words>();
        //    ListBEnum.MoveNext();
        //    while (ListAEnum.MoveNext() == true)
        //    {
        //        itemA = (Words)ListAEnum.Current;
        //        itemB = (Words)ListBEnum.Current;
        //        if (!itemA.Equals(itemB)) {
        //            additional.Add(itemA);
        //        }
        //        Console.WriteLine(itemA.ToString() + "," + itemB.ToString());
        //    }
        //    return additional;
        //}
    }
}
