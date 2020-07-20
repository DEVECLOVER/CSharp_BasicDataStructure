using System;
namespace StackAndQueue
{
    public class Node<T>
    {
        private T _data;
        private Node<T> _next;

        public T Data { get { return _data; } set { _data = value; } }
        public Node<T> Next { get { return _next; } set { _next = value; } }
        public Node()
        {
            _data = default(T);
            _next = null;
        }
        public Node(T item)
        {
            _data = item;
            _next = null;
        }
        public Node(Node<T> next)
        {
            _next = next;
        }
        public Node(T item,Node<T> next)
        {
            _data = item;
            Next = next;
        }
    }
    public class LinkStack<T> : IStack<T> 
    {
        private Node<T> _top;
        private int _count;
        public Node<T> Top { get { return _top; } set { _top = value; } }
        public int Count { get { return _count; } }
        public LinkStack()
        {
            _top = null;
            _count = 0;
        }
        public LinkStack(params T[] list)
        {
            for (int i = 0; i < list.Length ; i++)
            {
                Top = new Node<T>() { Data = list[i], Next = Top };
            }
        }
        public void Clear()
        {
            _top = null;
            _count = 0;
        }
        public int GetLength()
        {
            return _count;
        }
        public T GetTop()
        {
            if (IsEmpty())
            {
                return default(T);
            }
            return _top.Data;
        }
        public bool IsEmpty()
        {
            if (_top == null)
            {
                return true;
            }
            return false;
        }
        public T Pop()
        {
            if (IsEmpty())
            {
                return default(T);
            }
            T data = _top.Data;
            _top = _top.Next;
            _count--;
            return data;
        }
        public void Push(T item)
        {
            Node<T> newItem = new Node<T>(item);
            newItem.Next = _top;
            _top = newItem;
            _count++;
        }
        static T GetLastDataInStack(LinkStack<T> stack) 
        {
            T topData = stack.Pop();
            if (stack.IsEmpty())
            {
                return topData;
            }
            T lastData = GetLastDataInStack(stack);
            stack.Push(topData);
            return lastData;
        }
        public static void ReverseStack(ref LinkStack<T> stack)
        {
            if (stack.IsEmpty())
            {
                return;
            }
            T midData = GetLastDataInStack(stack);
            ReverseStack(ref stack);
            stack.Push(midData);
        }
        public static void ShowList(Node<T> top)
        {
            if (top == null)
            {
                return;
            }
            while (top != null)
            {
                Console.Write($"{top.Data}\t");
                top = top.Next;
            }
            Console.WriteLine();
        }
    }
}
