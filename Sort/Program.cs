using System;
namespace Sort
{
    class Sort<T> where T : IComparable
    {
        public static bool Compare(T p, T q)
        {
            if (p.CompareTo(q) > 0)
            {
                return true;
            }
            return false;
        }
        public static void Swap(ref T p, ref T q)
        {
            T temp = p;
            p = q;
            q = temp;
        }
        public static void InsertSort(ref T[] p, bool isUp)
        {
            if (isUp)
            {
                for (int i = 0; i < p.Length; i++)
                {
                    T temp = p[i];
                    int j = 0;
                    for (j = i - 1; j >= 0; j--)
                    {
                        if (!Compare(temp, p[j]))
                        {
                            p[j + 1] = p[j];
                        }
                        else
                        {
                            break;
                        }
                    }
                    p[j + 1] = temp;
                }
            }
            else
            {
                for (int i = 0; i < p.Length; i++)
                {
                    T temp = p[i];
                    int j = 0;
                    for (j = i - 1; j >= 0; j--)
                    {
                        if (Compare(temp, p[j]))
                        {
                            p[j + 1] = p[j];
                        }
                        else
                        {
                            break;
                        }
                    }
                    p[j + 1] = temp;
                }
            }
        }
        public static void BubbleSort(ref T[] p, bool isUp)
        {
            if (isUp)
            {
                for (int i = 0; i < p.Length - 1; i++)
                {
                    for (int j = 0; j < p.Length - i - 1; j++)
                    {
                        if (Compare(p[j], p[j + 1]))
                        {
                            Swap(ref p[j + 1], ref p[j]);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < p.Length - 1; i++)
                {
                    for (int j = 0; j < p.Length - i - 1; j++)
                    {
                        if (Compare(p[j + 1], p[j]))
                        {
                            Swap(ref p[j + 1], ref p[j]);
                        }
                    }
                }

            }
        }
        public static void SelectSort(ref T[] p, bool isUp)
        {
            if (isUp)
            {
                for (int i = 0; i < p.Length; i++)
                {
                    int index = i;
                    for (int j = i + 1; j < p.Length; j++)
                    {
                        if (Compare(p[index], p[j]))
                        {
                            index = j;
                        }
                    }
                    Swap(ref p[i], ref p[index]);
                }
            }
            else
            {
                for (int i = 0; i < p.Length; i++)
                {
                    int index = i;
                    for (int j = i; j < p.Length; j++)
                    {
                        if (!Compare(p[index], p[j]))
                        {
                            index = j;
                        }
                    }
                    Swap(ref p[i], ref p[index]);
                }

            }
        }
        public static void QuickSort(ref T[] p, int left, int right, bool isUp = true)
        {
            if (left >= right)
            {
                return;
            }
            T temp = p[left];
            int i = left;
            int j = right;
            if (isUp)
            {
                while (i < j)
                {
                    while ((j > i) && !Compare(temp, p[j]))
                    {
                        j--;
                    }
                    while ((i < j) && !Compare(p[i], temp))
                    {
                        i++;
                    }
                    Swap(ref p[i], ref p[j]);
                }
                p[left] = p[i];
                p[i] = temp;
                QuickSort(ref p, left, i - 1);
                QuickSort(ref p, i + 1, right);
            }
            else
            {
                while (i < j)
                {
                    while ((j > i) && !Compare(p[j], temp))//判断条件不可转变为  Compare(temp, p[j]) 注意等号的作用
                    {
                        j--;
                    }
                    while ((i < j) && !Compare(temp, p[i]))
                    {
                        i++;
                    }
                    Swap(ref p[i], ref p[j]);
                }
                p[left] = p[i];
                p[i] = temp;
                QuickSort(ref p, left, i - 1, false);
                QuickSort(ref p, i + 1, right, false);
            }

        }
        public static void Partition(ref int[] p, int start, int end)
        {
            if (start >= end)
            {
                return;
            }
            int left = start;
            int right = end;
            int temp = p[start];
            while (left < right)
            {
                while (left < right && p[right] >= temp)
                {
                    --right;
                }
                p[left] = p[right];
                while (left < right && p[left] <= temp)
                {
                    ++left;
                }
                p[right] = p[left];
            }
            p[start] = p[left];
            p[left] = temp;
            Partition(ref p, start, left - 1);
            Partition(ref p, left + 1, end);
        }
        public static void Show(ref T[] p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                Console.Write($"{p[i]}\t");
            }
            Console.WriteLine("\n**********************");
        }
        public static void TestSort()
        {
            double[] test = { 1.01, 0.5, 2.3, -6.6, 9, 8, 0, -0.1, 2, 3,2,2 };
            Sort<double>.InsertSort(ref test, true);
            Sort<double>.Show(ref test);
            Sort<double>.InsertSort(ref test, false);
            Sort<double>.Show(ref test);
            Sort<double>.BubbleSort(ref test, true);
            Sort<double>.Show(ref test);
            Sort<double>.BubbleSort(ref test, false);
            Sort<double>.Show(ref test);
            Sort<double>.SelectSort(ref test, true);
            Sort<double>.Show(ref test);
            Sort<double>.SelectSort(ref test, false);
            Sort<double>.Show(ref test);
            Sort<double>.QuickSort(ref test, 0, test.Length - 1);
            Sort<double>.Show(ref test);
            Sort<double>.QuickSort(ref test, 0, test.Length - 1, false);
            Sort<double>.Show(ref test);
            int[] numbers = { 1, 2, 3, 2, 2,4,2};
            Sort<double>.Partition(ref numbers, 0, numbers.Length - 1);
            Sort<int>.Show(ref numbers);


        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Sort<double>.TestSort();
            Console.Read();

        }
    }
}
