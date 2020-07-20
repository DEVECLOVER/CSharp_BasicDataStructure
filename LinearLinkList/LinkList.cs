using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearList.SDk;
namespace LinearLinkList
{
    class Node<T>
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
    public class LinkList<T> : ILinearList<T> where T : IComparable
    {
        private Node<T> _head;
        private Node<T> Head { get { return _head; } set { _head = value; } }
        private int ListLength = 0;
        public LinkList()
        {
            _head = null;
        }
        /// <summary>
        /// 妙不可言
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        private static Node<T> RecursionReverse(Node<T> head)
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
        public static void Reverse(ref LinkList<T> list)
        {
            Node<T> tempNode = list._head;
            Node<T> newNode = new Node<T>();
            list._head = null;
            while (tempNode != null)
            {
                newNode = tempNode;
                tempNode = tempNode.Next;
                newNode.Next = list._head;
                list._head = newNode;
                //Console.WriteLine($"{list._tail.Data}");
            }

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
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void AddElement(T item)
        {
            Node<T> pHead = new Node<T>(item);
            Node<T> pIndex = new Node<T>();
            if (Head == null)
            {
                Head = pHead;
                ListLength++;
                return;
            }
            pIndex = _head;
            //Console.WriteLine(Head.Data);
            //pIndex.Data = default(T);
            //Console.WriteLine(Head.Data);
            while (pIndex.Next != null) //这里将.Next去掉是不行的，仔细分析
            {
                pIndex = pIndex.Next;
            }
            pIndex.Next = pHead;
            ListLength++;
        }

        public void ChangeElement(T item, int index)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _head = null;
            ListLength = 0;
        }

        public T GetElement(int index)
        {
            if (index < 1 || index > ListLength)
            {
                return default(T);
            }
            Node<T> pTemp = _head;
            for (int i = 0; i < index - 1; i++)
            {
                pTemp = pTemp.Next;
            }
            return pTemp.Data;
        }

        public int GetLength()
        {
            //Node<T> pointerIndex = _head;
            //int len = 0;
            //while (pointerIndex != null)
            //{
            //    len++;
            //    pointerIndex = pointerIndex.Next;
            //}
            //return len;
            return ListLength;
        }

        public void Insert(T item, int index)
        {
            if (IsEmpty() || index < 1 || index > ListLength)
            {
                return;
            }
            if (index == 1)
            {
                Node<T> newItem = new Node<T>(item);
                newItem.Next = _head;
                _head = newItem;
                ListLength++;
                return;
            }
            Node<T> pItem = new Node<T>(item);
            Node<T> pIndex = new Node<T>();
            pIndex = _head;
            for (int i = 1; i < index - 1; i++)
            {
                pIndex = pIndex.Next;
            }
            pItem.Next = pIndex.Next;
            pIndex.Next = pItem;
            ListLength++;
            return;

        }

        public bool IsEmpty()
        {
            if (_head == null)
            {
                return true;
            }
            return false;
        }

        public int Locate(T item)
        {
            if (IsEmpty())
            {
                return -1;
            }
            int index = 0;
            Node<T> pIndex = _head;
            while (pIndex != null)
            {
                index++;
                if (pIndex.Data.Equals(item))
                {
                    return index;
                }
                pIndex = pIndex.Next;
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
                return;
            }
            if (_head.Data.Equals(item))
            {
                _head = _head.Next;
                ListLength--;
                return;
            }
            Node<T> pIndex = _head;
            while (pIndex.Next != null)
            {
                if (pIndex.Next.Data.Equals(item))
                {
                    pIndex.Next = pIndex.Next.Next;
                    ListLength--;
                    return;
                }
                pIndex = pIndex.Next;
            }
            if (pIndex.Next == null)
            {
                Console.WriteLine($"元素{item}不存在");
                return;
            }
        }
        /// <summary>
        /// 如何获取泛型类的指针
        /// </summary>
        /// <param name="list"></param>
        public static void ShowList(LinkList<T> list)
        {
            Node<T> pTemp = list.Head;
            if (pTemp == null)
            {
                return;
            }
            while (pTemp != null)
            {
                //unsafe
                //{
                //    Node<T>* pp = &pTemp;
                //}
                //Console.WriteLine($"元素地址为{pTemp.ToString()}");
                Console.WriteLine(pTemp.Data);
                pTemp = pTemp.Next;
            }
        }
        /// <summary>
        /// 如何尽可能简单的实现倒置 不创造新的链表 使用数组呢？
        /// 创造性地将顺序表和链表结合起来
        /// </summary>
        /// <param name="list"></param>
        //public static void ReverseLinkList(ref LinkList<T> list)
        //{
        //    if (list.IsEmpty())
        //    {
        //        return;
        //    }
        //    SeqList<T> newSeqList = new SeqList<T>();
        //    Node<T> pTemp = list.Head;
        //    while (pTemp != null)
        //    {
        //        //unsafe
        //        //{
        //        //    Node<T>* pp = &pTemp;
        //        //}
        //        newSeqList.AddElement(pTemp.Data);
        //        pTemp = pTemp.Next;
        //    }
        //    pTemp = list.Head;
        //    int len = newSeqList.GetLength();
        //    for (int i = 0; i < len; i++)
        //    {
        //        pTemp.Data = newSeqList[len - i - 1];
        //        pTemp = pTemp.Next;
        //    }
        //}

        public static void MergeLinkList(LinkList<T> list1, LinkList<T> list2, out LinkList<T> list3)
        {
            list3 = new LinkList<T>();
            Node<T> pFormer = list1.Head;
            Node<T> pLatter = list2.Head;
            while (pFormer != null && pLatter != null)
            {
                if (LinkList<T>.Compare(pFormer.Data, pLatter.Data))
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


}
