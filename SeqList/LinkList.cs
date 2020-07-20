using System;
using System.Collections.Generic;

namespace SeqList
{
    public class Node<T>
    {
        private T _data;
        private Node<T> _next;
        public Node()
        {
            _data = default(T);
            _next = null;
        }
        public Node(T data)
        {
            _data = data;
            _next = null;
        }
        public Node(Node<T> node)
        {
            _next = node;
        }
        public Node(T data, Node<T> node)
        {
            _data = data;
            _next = node;
        }
        public T Data { get { return _data; } set { _data = value; } }
        public Node<T> Next { get { return _next; } set { _next = value; } }
    }
    class NoHeadLinkList<T> : IListDS<T> where T : IComparable
    {
        private Node<T> _head;
        public Node<T> Head { get { return _head; } set { _head = value; } }
        private int ListLength;
        public NoHeadLinkList()
        {
            _head = null;
            ListLength = 0;
        }
        public NoHeadLinkList(params T[] list)
        {
            Head = null;
            for (int i = list.Length - 1; i >= 0; i--)
            {
                Head = new Node<T>(list[i], Head);
                ListLength++;
            }
        }
        public static void ShowMenu()
        {
            Console.WriteLine("**************************");
            Console.WriteLine("1.添加元素");
            Console.WriteLine("2.显示元素");
            Console.WriteLine("3.查找元素");
            Console.WriteLine("4.删除元素");
            Console.WriteLine("5.改变元素");
            Console.WriteLine("6.元素个数");
            Console.WriteLine("7.清空元素");
            Console.WriteLine("8.判断表是否为空");
            Console.WriteLine("9.返回位置的元素");
            Console.WriteLine("10.插入元素");
            Console.WriteLine("0.退出系统");
            Console.WriteLine("请输入功能选项：");
        }
        /// <summary>
        /// 递归翻转链表，很妙！
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static Node<T> RecursionReverse(Node<T> head)
        {
            if (head == null || head.Next == null)
            {
                return head;
            }
            Node<T> tempNode = RecursionReverse(head.Next);
            head.Next.Next = head;
            head.Next = null;
            return tempNode;
        }
        /// <summary>
        /// 循环翻转链表
        /// </summary>
        /// <param name="list"></param>
        public static Node<T> Reverse(Node<T> head)
        {
            Node<T> tempNode = head;
            Node<T> newNode = new Node<T>();
            head = null;
            while (tempNode != null)
            {
                newNode = tempNode;
                tempNode = tempNode.Next;
                newNode.Next = head;
                head = newNode;
            }
            return head;
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
                return GetElement(index);
            }

            set
            {
                ChangeElement(value, index);
            }
        }
        public void AddElement(T item)
        {

            Node<T> newNode = new Node<T>(item);
            if (Head == null)
            {
                Head = newNode;
                ListLength++;
                return;
            }
            Node<T> tempNode = new Node<T>();
            tempNode = Head;
            while (tempNode.Next != null) //这里将.Next去掉是不行的，仔细分析
            {
                tempNode = tempNode.Next;
            }
            tempNode.Next = newNode;
            ListLength++;
        }
        public void ChangeElement(T item, int index)
        {
            if (IsEmpty())
            {
                Console.WriteLine("连表为空");
                return;
            }
            if (index < 0 || index >= ListLength)
            {
                Console.WriteLine("越界");
                return;
            }
            int nodeIndex = 0;
            Node<T> tempNode = Head;
            while (nodeIndex < index)
            {
                nodeIndex++;
                tempNode = tempNode.Next;
            }
            tempNode.Data = item;
        }
        public void Clear()
        {
            _head = null;
            ListLength = 0;
        }
        public T GetElement(int index)
        {
            if (index < 0 || index >= ListLength)
            {
                Console.WriteLine("越界");
                return default(T);
            }
            Node<T> tempNode = Head;
            for (int i = 0; i < index; i++)
            {
                tempNode = tempNode.Next;
            }
            return tempNode.Data;
        }
        public int GetLength()
        {
            return ListLength;
        }
        public void Insert(T item, int index)
        {
            if (IsEmpty())
            {
                Console.WriteLine("表为空");
                return;
            }
            if (index < 0 || index >= ListLength)
            {
                Console.WriteLine("标签越界");
                return;
            }
            Node<T> newNode = new Node<T>(item);
            if (index == 0)
            {
                newNode.Next = Head;
                Head = newNode;
                ListLength++;
                return;
            }
            Node<T> tempNode = Head;
            for (int i = 0; i < index - 1; i++)
            {
                tempNode = tempNode.Next;
            }
            newNode.Next = tempNode.Next;
            tempNode.Next = newNode;
            ListLength++;
            return;

        }
        public bool IsEmpty()
        {
            if (Head == null && ListLength == 0)
            {
                return true;
            }
            return false;
        }
        public int Locate(T item)
        {
            if (IsEmpty())
            {
                Console.WriteLine("表为空");
                return -1;
            }
            int index = 0;
            Node<T> tempNode = Head;
            while (tempNode != null)
            {
                if (tempNode.Data.Equals(item))
                {
                    //返回第一个匹配下标
                    return index;
                }
                tempNode = tempNode.Next;
                index++;
            }
            return -1;
        }
        /// <summary>
        /// 传统删除做法都是用双指针，在这里只用单指针
        /// </summary>
        /// <param name="item"></param>
        public void RemoveElement(T item)
        {
            if (IsEmpty())
            {
                Console.WriteLine("表为空");
                return;
            }
            if (Head.Data.Equals(item))
            {
                Head = Head.Next;
                ListLength--;
                return;
            }
            Node<T> tempNode = Head;
            while (tempNode.Next != null)
            {
                if (tempNode.Next.Data.Equals(item))
                {
                    tempNode.Next = tempNode.Next.Next;
                    ListLength--;
                    return;
                }
                tempNode = tempNode.Next;
            }
            if (tempNode.Next == null)
            {
                Console.WriteLine($"元素{item}不存在");
                return;
            }
        }
        /// <summary>
        /// 如何获取泛型类的指针
        /// </summary>
        /// <param name="list"></param>
        public static void ShowList(Node<T> head)
        {
            if (head == null)
            {
                Console.WriteLine("表为空");
                return;
            }
            Node<T> tempNode = head;
            while (tempNode != null)
            {
                Console.Write($"{tempNode.Data}\t");
                tempNode = tempNode.Next;
            }
            Console.WriteLine();
        }
        /// <summary>
        /// 如何尽可能简单的实现倒置 不创造新的链表 使用数组呢？
        /// 创造性地将顺序表和链表结合起来
        /// </summary>
        /// <param name="list"></param>
        public static void ReverseLinkList(Node<T> head)
        {
            if (head == null)
            {
                Console.WriteLine("表为空");
                return;
            }
            SeqList<T> newSeqList = new SeqList<T>();
            Node<T> tempNode = head;
            while (tempNode != null)
            {
                newSeqList.AddElement(tempNode.Data);
                tempNode = tempNode.Next;
            }
            tempNode = head;
            int len = newSeqList.GetLength();
            for (int i = 0; i < len; i++)
            {
                tempNode.Data = newSeqList[len - i - 1];
                tempNode = tempNode.Next;
            }
        }
        public static void MergeLinkList(Node<T> head1, Node<T> head2, out NoHeadLinkList<T> list3)
        {
            list3 = new NoHeadLinkList<T>();
            Node<T> pFormer = head1;
            Node<T> pLatter = head2;
            while (pFormer != null && pLatter != null)
            {
                if (NoHeadLinkList<T>.Compare(pFormer.Data, pLatter.Data))
                {
                    list3.AddElement(pLatter.Data);
                    pLatter = pLatter.Next;
                }
                else
                {
                    list3.AddElement(pFormer.Data);
                    pFormer = pFormer.Next;
                }
            }
            while (pFormer != null)
            {
                list3.AddElement(pFormer.Data);
                pFormer = pFormer.Next;
            }
            while (pLatter != null)
            {
                list3.AddElement(pLatter.Data);
                pLatter = pLatter.Next;
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    public class HeadLinkList<T> : IListDS<T> 
    {
        private Node<T> _head;
        private Node<T> _tail;
        private int ListLength = 0;
        public Node<T> Head { get { return _head; } set { _head = value; } }
        public HeadLinkList()
        {
            _head = new Node<T>();
            _tail = _head.Next;
        }
        public HeadLinkList(params T[] nums)
        {
            _head = new Node<T>();
            for (int i = 0; i < nums.Length; i++)
            {
                AddElement(nums[i]);
            }
        }
        public static void ShowMenu()
        {
            Console.WriteLine("**************************");
            Console.WriteLine("1.添加元素");
            Console.WriteLine("2.显示元素");
            Console.WriteLine("3.查找元素");
            Console.WriteLine("4.删除元素");
            Console.WriteLine("5.改变元素");
            Console.WriteLine("6.元素个数");
            Console.WriteLine("7.清空元素");
            Console.WriteLine("8.判断表是否为空");
            Console.WriteLine("9.返回位置的元素");
            Console.WriteLine("10.插入元素");
            Console.WriteLine("0.退出系统");
            Console.WriteLine("请输入功能选项：");
        }
        public void AddElement(T item)
        {
            Node<T> tempnode = _head;
            while (tempnode.Next != null)
            {
                tempnode = tempnode.Next;
            }
            tempnode.Next = new Node<T>(item);                                    
            ListLength++;
        }
        public static Node<T> RecursionReverse(Node<T> head)
        {
            if (head == null || head.Next == null)
            {
                return head;
            }
            Node<T> tempNode = RecursionReverse(head.Next);
            head.Next.Next = head;
            head.Next = null;
            return tempNode;
        }
        public static Node<T> Reverse(Node<T> head)
        {
            Node<T> tempNode = head.Next;
            Node<T> newNode = new Node<T>();
            head.Next = null;
            while (tempNode != null)
            {
                newNode = tempNode;
                tempNode = tempNode.Next;
                newNode.Next = head.Next;
                head.Next = newNode;
            }
            return head;
        }
        public T this[int index]
        {
            get
            {
                return GetElement(index);
            }

            set
            {
                ChangeElement(value, index);
            }
        }
        public void ChangeElement(T item, int index)
        {
            if (IsEmpty())
            {
                Console.WriteLine("表为空");
                return;
            }
            if (index < 0 || index >= ListLength)
            {
                Console.WriteLine("标签越界");
                return;
            }
            Node<T> tempNode = _head.Next;
            for (int i = 0; i < index; i++)
            {
                tempNode = tempNode.Next;
            }
            tempNode.Data = item;
        }
        public void Clear()
        {
            Head.Next = null;
            ListLength = 0;
        }
        public T GetElement(int index)
        {
            if (IsEmpty())
            {
                Console.WriteLine("表为空");
                return default(T);
            }
            if (index < 0 || index >= ListLength)
            {
                Console.WriteLine("标签越界");
                return default(T);
            }
            Node<T> tempNode = Head.Next;
            for (int i = 0; i < index ; i++)
            {
                tempNode = tempNode.Next;
            }
            return tempNode.Data;
        }
        public int GetLength()
        {
            return ListLength;
        }
        public void Insert(T item, int index)
        {
            if (index < 0 || index > ListLength)
            {
                Console.WriteLine("标签越界");
                return;
            }
            Node<T> newNode = new Node<T>(item);
            Node<T> tempNode = Head;
            for (int i = 0; i < index; i++)
            {
                tempNode = tempNode.Next;
            }
            newNode.Next = tempNode.Next;
            tempNode.Next = newNode;
            ListLength++;
        }
        public bool IsEmpty()
        {
            if (Head.Next == null && ListLength == 0)
            {
                return true;
            }
            return false;
        }
        public int Locate(T item)
        {
            if (IsEmpty())
            {
                Console.WriteLine("表为空");
                return -1;
            }
            int index = -1;
            Node<T> tempNode = Head.Next;
            while (tempNode != null)
            {
                index++;
                if (tempNode.Data.Equals(item))
                {
                    return index;
                }
                tempNode = tempNode.Next;
            }
            return -1;
        }
        public List<int> FullMatch(T item)
        {
            if (IsEmpty())
            {
                Console.WriteLine("表为空");
                return null;
            }
            List<int> match = new List<int>();
            int index = -1;
            Node<T> tempNode = Head.Next;
            while (tempNode != null)
            {
                index++;
                if (tempNode.Data.Equals(item))
                {
                    match.Add(index);
                }
                tempNode = tempNode.Next;
            }
            return match;
        }
        public void RemoveElement(T item)
        {
            if (Locate(item) == -1)
            {
                Console.WriteLine("元素不存在");
                return;
            }
            RemoveElementAt(Locate(item));
        }
        public void RemoveElementAt(int index)
        {
            Node<T> tempNode = Head;
            for (int i = 0; i < index; i++)
            {
                tempNode = tempNode.Next;
            }
            tempNode.Next = tempNode.Next.Next;
            ListLength--;
        }
        public static void ShowList(Node<T> head)
        {
            if (head.Next == null)
            {
                Console.WriteLine("表为空");
                return;
            }
            Node<T> tempNode = head.Next;
            while (tempNode != null)
            {
                Console.Write(tempNode.Data + " ");
                tempNode = tempNode.Next;
            }
            Console.WriteLine();
        }
    }
    class CircleLinkList<T> : IListDS<T> where T : IComparable
    {
        private Node<T> _head;
        private Node<T> _tail;
        private int ListLength;
        public CircleLinkList()
        {
            _head = new Node<T>();
            _tail = new Node<T>();
            _head.Next = _tail;
            _tail.Next = _head;
        }
        public T this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void AddElement(T item)
        {
            Node<T> newItem = new Node<T>(item);
            Node<T> tempNode = _tail;
            while (tempNode.Next != _head)
            {
                tempNode = tempNode.Next;
            }
            newItem.Next = _head;
            tempNode.Next = newItem;
            ListLength++;
        }

        public void ChangeElement(T item, int index)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _tail.Next = _head;
            ListLength = 0;
        }

        public T GetElement(int index)
        {
            throw new NotImplementedException();
        }

        public int GetLength()
        {
            return ListLength;
        }

        public void Insert(T item, int index)
        {
            if (IsEmpty() || index < 1 || index > ListLength)
            {
                return;
            }
            Node<T> newItem = new Node<T>(item);
            Node<T> tempNode = _tail;
            for (int i = 0; i < index - 1; i++)
            {
                tempNode = tempNode.Next;
            }
            newItem.Next = tempNode.Next;
            tempNode.Next = newItem;
            ListLength++;
        }

        public bool IsEmpty()
        {
            if (_tail.Next == _head)
            {
                return true;
            }
            return false;
        }

        public int Locate(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveElement(T item)
        {
            if (IsEmpty())
            {
                return;
            }
            Node<T> tempNode = _tail;
            while (tempNode.Next != _head)
            {
                if (tempNode.Next.Data.Equals(item))
                {
                    tempNode.Next = tempNode.Next.Next;
                    return;
                }
                tempNode = tempNode.Next;
            }
            if (tempNode.Next == _head)
            {
                Console.WriteLine("元素不存在");
            }

        }
        public static void ShowList(CircleLinkList<T> list)
        {
            Node<T> tempNode = list._tail;
            while (tempNode.Next != list._head)
            {
                Console.WriteLine(tempNode.Next.Data);
                tempNode = tempNode.Next;
            }
        }
        public static void CircleShow(CircleLinkList<T> list, int num)
        {
            Node<T> tempNode = list._tail;
            int Count = (list.GetLength() + 2) * num - 2;
            for (int i = 0; i < Count; i++)
            {
                tempNode = tempNode.Next;
                if (tempNode == list._head || tempNode == list._tail)
                {
                    continue;
                }
                Console.WriteLine(tempNode.Data);
            }
        }
    }
}
