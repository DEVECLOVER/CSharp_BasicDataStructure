using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeqList
{
    class DbNode<T>
    {
        private T _data;
        private DbNode<T> _prev;
        private DbNode<T> _next;
        public T Data { get { return _data; } set { _data = value; } }
        public DbNode<T> Prev { get { return _prev; } set { _prev = value; } }
        public DbNode<T> Next { get { return _next; } set { _next = value; } }
        public DbNode()
        {
            _data = default(T);
            _prev = null;
            _next = null;
        }
        public DbNode(T data)
        {
            _data = data;
            _prev = null;
            _next = null;

        }
        public DbNode(DbNode<T> next)
        {
            _next = next;
        }

        public DbNode(T data,DbNode<T> prev, DbNode<T> next)
        {
            _data = data;
            _prev = prev;
            _next = next;
        }


    }
    class DbLinkList<T> : IListDS<T> where T : IComparable 
    {
        private DbNode<T> _head;
        private DbNode<T> _tail;
        public DbNode<T> Head { get { return _head; } set { _head = value; } }
        public int ListLength = 0;
        public DbLinkList()
        {
            _head = new DbNode<T>();
            _tail = new DbNode<T>();
            _head.Next = _tail;
            _tail.Prev = _head;
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
            DbNode<T> newItem = new DbNode<T>(item);
            DbNode<T> tempNode = _tail;
            while (tempNode.Next != null)
            {
                tempNode = tempNode.Next;
            }
            newItem.Prev = tempNode;
            tempNode.Next = newItem;
            ListLength++;

        }

        public void ChangeElement(T item, int index)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _tail.Next = null;
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
            DbNode<T> newItem = new DbNode<T>(item);
            DbNode<T> tempNode = _tail;
            for (int i = 0; i < index - 1; i++)
            {
                tempNode = tempNode.Next;
            }
            tempNode.Next.Prev = newItem;
            newItem.Next = tempNode.Next;
            newItem.Prev = tempNode;
            tempNode.Next = newItem;
            ListLength++;
        }

        public bool IsEmpty()
        {
            if (_tail.Next == null)
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
            DbNode<T> tempNode = _tail;
            while (tempNode.Next != null)
            {
                if (tempNode.Next.Data.Equals(item))
                {
                    tempNode.Next.Next.Prev = tempNode;
                    tempNode.Next = tempNode.Next.Next;
                    ListLength--;
                    return;
                }
                tempNode = tempNode.Next;
            }
            if (tempNode.Next != null)
            {
                Console.WriteLine("元素不存在");
            }
        }
        public static void ShowList(DbLinkList<T> list)
        {
            DbNode<T> tempNode = list._tail;
            while (tempNode.Next != null)
            {
                Console.WriteLine(tempNode.Next.Data);
                tempNode = tempNode.Next;
            }
        }
    }
}
