using System;
namespace StackAndQueue
{
   public class LinkQueue<T> : IQueue<T> 
    {
        private Node<T> _front;
        private Node<T> _rear;
        private int _count;
        public int Count { get { return _count; } }
        public Node<T> Front { get { return _front; } set { _front = value; } }
        public Node<T> Rear { get { return _rear; } set { _rear = value; } }
        public LinkQueue()
        {
            _front = null;
            _rear = null;
        }
        public LinkQueue(params T[] list)
        {
            Node<T> tempNode = null;
            Rear = null;
            for (int i = list.Length - 1; i >= 0; i--)
            {
                _count++;
                Rear = new Node<T>(list[i],Rear);
                if (i == list.Length - 1)
                {
                    tempNode = Rear;
                }
            }
            Front = Rear;
            Rear = tempNode;
        }
        public void Clear()
        {
            _front = null;
            _rear = null;
            _count = 0;
        }
        public T GetFront()
        {
            if (IsEmpty())
            {
                Console.WriteLine("队列为空");
                return default(T);
            }
            return _front.Data;
        }             
        public int GetLength()
        {
            return _count;
        }
        public void In(T item)
        {
            Node<T> newItem = new Node<T>(item);
            if (IsEmpty())
            {
                _rear = newItem;
                _front = newItem;
            }
            else
            {
                _rear.Next = newItem;
                _rear = newItem;
            }
            _count++;
        }
        public bool IsEmpty()
        {
            if (_front == _rear && _count == 0)
            {
                return true;
            }
            return false;
        }
        public T Out()
        {
            if (IsEmpty())
            {
                return default(T);
            }
            T temp = _front.Data;
            _front = _front.Next;
            _count--;
            if (_count == 0)
            {
                _front = null;
                _rear = null;
            }
            return temp;
        }
        public static void ReverseQueue(ref LinkQueue<T> queue)
        {
            if (queue.IsEmpty())
            {
                return;
            }
            T frontData = queue.Out();
            ReverseQueue(ref queue);
            queue.In(frontData);
        }
        public static void ShowList(LinkQueue<T> list)
        {
            Node<T> tempNode = list.Front;
            while (tempNode != null)
            {
                Console.Write($"{tempNode.Data}\t");
                tempNode = tempNode.Next;
            }
            Console.WriteLine();
        }
    }
}
