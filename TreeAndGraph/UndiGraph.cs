using System;
namespace TreeAndGraph
{
    class GraphNode<T>
    {
        private T _data;
        public T Data { get { return _data; } set { _data = value; } }
        public GraphNode(T data)
        {
            _data = data;
        }
    }
    class GraphAdjMatrix<T> : IGraph<T>
    {
        private GraphNode<T>[] _nodes;
        private int _numEdges;
        private int[,] _matrix;
        public GraphNode<T>[] Nodes { get { return _nodes; }}
        public int[,] Matrix { get { return _matrix; } }
        public GraphAdjMatrix(int num)
        {
            _nodes = new GraphNode<T>[num];
            _matrix = new int[num, num];
            _numEdges = 0;
        }


        public int GetNumOfVertex()
        {
            return _nodes.Length;
        }

        public int GetNumOfEdge()
        {
            return _numEdges;
        }

        public void SetEdge(GraphNode<T> v1, GraphNode<T> v2, int v)
        {
            if (!IsNode(v1) || !IsNode(v2))
            {
                return;
            }
            _matrix[GetIndex(v1), GetIndex(v2)] = v;
            _matrix[GetIndex(v2), GetIndex(v1)] = v;
            _numEdges++;
        }

        public void DelEdge(GraphNode<T> v1, GraphNode<T> v2)
        {
            if (!IsNode(v1) || !IsNode(v2))
            {
                return;
            }
            if (_matrix[GetIndex(v1),GetIndex(v2)] != 0)
            {
                _matrix[GetIndex(v1), GetIndex(v2)] = 0;
                _matrix[GetIndex(v2), GetIndex(v1)] = 0;
                _numEdges--;
            }


        }

        public bool IsEdge(GraphNode<T> v1, GraphNode<T> v2)
        {
            if (!IsNode(v1) || !IsNode(v2))
            {
                return false;
            }
            if (_matrix[GetIndex(v1),GetIndex(v2)] != 0)
            {
                return true;
            }
            return false;

        }

        public bool IsNode(GraphNode<T> v)
        {
            foreach (var item in _nodes)
            {
                if (item.Equals(v))
                {
                    return true;
                }
            }
            return false;
        }

        public int GetIndex(GraphNode<T> v)
        {
            int index = -1;
            for (index = 0; index < _nodes.Length; index++)
            {
                if (_nodes[index].Equals(v))
                {
                    return index;
                }
            }
            return -1;
        }

        public int GetMatrix(int i, int j)
        {
            return _matrix[i, j];
        }

        public GraphNode<T> GetNode(int index)
        {
            return _nodes[index];
        }

        public void SetNode(GraphNode<T> v, int index)
        {
            _nodes[index] = v;
        }
    }
    class LinkGraphNode<T>
    {
        private T _data;
        private double _distance;
        private LinkGraphNode<T> _next;
        public double Distance { get { return _distance; } set { _distance = value; } }
        public T Data { get { return _data; } set { _data = value; } }
        public LinkGraphNode<T> Next { get { return _next; } set { _next = value; } }
        public LinkGraphNode()
        {
            _data = default(T);
            _distance = default(double);
            _next = null;
        }
        public LinkGraphNode(T data,double distance)
        {
            _data = data;
            _distance = distance;
            _next = null;
        }
    }
    class LinkGraphList<T>
    {
        private LinkGraphNode<T> _head;
        private LinkGraphNode<T> _tail;
        public LinkGraphNode<T> Tail { get { return _tail; } set { _tail = value; } }
        public LinkGraphList()
        {
            _head = new LinkGraphNode<T>();
            _tail = new LinkGraphNode<T>();
            _head.Next = _tail;
        }
        public void AddElement(T data,double distance)
        {
            LinkGraphNode<T> tempNode = _tail;
            while (tempNode.Next != null)
            {
                tempNode = tempNode.Next;
            }
            tempNode.Next = new LinkGraphNode<T>(data, distance);
        }
        public void ModifyValue(T data,double distance)
        {
            LinkGraphNode<T> tempNode = _tail.Next;
            while (tempNode != null)
            {
                if (tempNode.Data.Equals(data))
                {
                    tempNode.Distance = distance;
                    return;
                }
                tempNode = tempNode.Next;
            }
        }                                    
        public void Show(LinkGraphList<T> list)
        {
            LinkGraphNode<T> tempNode = list.Tail;
            while (tempNode.Next != null)
            {
                tempNode = tempNode.Next;
                Console.Write($"《{tempNode.Distance}+{tempNode.Data}");
            }
            Console.WriteLine("《null\n");
        }
    }
}
