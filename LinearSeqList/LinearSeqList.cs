using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearList.SDk;
namespace LinearSeqList
{
    public class LinearSeqList<T> : ILinearList<T> where T : IComparable
    {
        private const int MaxSize = 100;
        private int count = 0;
        private T[] data = new T[MaxSize];
        public LinearSeqList()
        {

        }

        public static bool Compare(T p, T q)
        {
            if (p.CompareTo(q) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public T this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                data[index] = value;
            }
        }

        public void AddElement(T item)
        {
            if (count == MaxSize)
            {
                return;
            }
            else
            {
                data[count] = item;
                ++count;
            }
        }


        public void ChangeElement(T item, int index)
        {
            if (index > MaxSize)
            {
                return;
            }
            else
            {
                data[index] = item;
            }
        }

        public void Clear()
        {
            if (IsEmpty())
            {
                return;
            }
            else
            {
                data = null;
                count = 0;
            }
        }

        public T GetElement(int index)
        {
            if (index >= 0 && index <= MaxSize - 1)
            {
                return data[index];
            }
            else
            {
                return default(T);
            }
        }

        public int GetLength()
        {
            return count;
        }

        public void Insert(T item, int index)
        {
            if (index < 1 || index >= MaxSize || count == MaxSize)
            {
                return;
            }
            for (int i = count - 1; i >= index; i--)
            {
                data[i + 1] = data[i];
            }
            data[index] = item;
            ++count;
        }

        public bool IsEmpty()
        {
            if (count == 0)
            {
                return true;
            }
            return false;
        }

        public int Locate(T item)
        {
            for (int i = 0; i < count; i++)
            {
                if (data[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void RemoveElement(T item)
        {
            int tempIndex = 0;
            for (int i = 0; i < count; i++)
            {
                if (data[i].Equals(item))
                {
                    tempIndex = i;
                    break;
                }
            }
            for (int i = tempIndex; i <= count - 2; i++)
            {
                data[i] = data[i + 1];
            }
            --count;
        }

        public static void ReverseSeqList(ref LinearSeqList<T> list)
        {
            T tempNum = default(T);
            int count = list.GetLength() - 1;
            for (int i = 0; i <= count / 2; i++)
            {
                tempNum = list[i];
                list[i] = list[count - i];
                list[count - i] = tempNum;
            }
        }
        public static void ShowSeqList(LinearSeqList<T> list)
        {
            for (int i = 0; i < list.GetLength(); i++)
            {
                Console.WriteLine(list[i]);
            }
        }

        /// <summary>
        /// 如何定义泛型类型的运算符重载
        /// </summary>
        /// <param name="seqlist1"></param>
        /// <param name="seqlist2"></param>
        /// <param name="seqlist3"></param>
        public static void MergeSeqList(LinearSeqList<T> seqlist1, LinearSeqList<T> seqlist2, out LinearSeqList<T> seqlist3)
        {
            seqlist3 = new LinearSeqList<T>();
            int index1 = 0;
            int index2 = 0;
            int index3 = 0;
            int len1 = seqlist1.GetLength() - 1;
            int len2 = seqlist2.GetLength() - 1;
            while (index1 <= len1 && index2 <= len2)
            {
                if (LinearSeqList<T>.Compare(seqlist1[index1], seqlist2[index2]))
                {
                    //seqlist3.Insert(seqlist2[index2], index3);
                    seqlist3.AddElement(seqlist2[index2]);
                    index3++;
                    index2++;
                }
                else
                {
                    //seqlist3.Insert(seqlist1[index1], index3);
                    seqlist3.AddElement(seqlist1[index1]);
                    index3++;
                    index1++;
                }

            }
            while (index1 <= len1)
            {
                seqlist3.Insert(seqlist1[index1], index3);
                index3++;
                index1++;
            }
            while (index2 <= len2)
            {
                seqlist3.Insert(seqlist2[index2], index3);
                index3++;
                index2++;
            }

        }

        public override string ToString()
        {
            return base.ToString();
        }


    }
}
