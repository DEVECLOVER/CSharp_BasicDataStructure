using System;
using System.Collections.Generic;
namespace TreeAndGraph
{
    class ExNode<T>
    {
        private Node<T> _data;
        private ExNode<T> _next;

        public Node<T> Data { get { return _data; } set { _data = value; } }
        public ExNode<T> Next { get { return _next; } set { _next = value; } }

        public ExNode()
        {
            _data = default(Node<T>);
            _next = null;
        }
        public ExNode(Node<T> item)
        {
            _data = item;
            _next = null;
        }
        public ExNode(ExNode<T> next)
        {
            _next = next;
        }

    }
    class Node<T>
    {
        private T _data;
        private Node<T> _leftChild;
        private Node<T> _rightChild;
        public T Data { get { return _data; } set { _data = value; } }
        public Node<T> LeftChild { get { return _leftChild; } set { _leftChild = value; } }
        public Node<T> RightChild { get { return _rightChild; } set { _rightChild = value; } }
        public Node()
        {
            _data = default(T);
            _leftChild = null;
            _rightChild = null;
        }
        public Node(T data, Node<T> leftchild, Node<T> rightchild)
        {
            _data = data;
            _leftChild = leftchild;
            _rightChild = rightchild;
        }
        public Node(T data)
        {
            _data = data;
            _leftChild = null;
            _rightChild = null;
        }
        public Node(Node<T> leftchild, Node<T> rightchild)
        {
            _data = default(T);
            _leftChild = leftchild;
            _rightChild = rightchild;
        }

    }
    class LinkStack<T>
    {
        private ExNode<T> _top;
        private int _count;
        public ExNode<T> Top { get { return _top; } set { _top = value; } }
        public int Count { get { return _count; } }
        public LinkStack()
        {
            _top = null;
            _count = 0;
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

        public Node<T> GetTop()
        {
            if (IsEmpty())
            {
                return default(Node<T>);
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

        public Node<T> Pop()
        {
            if (IsEmpty())
            {
                return default(Node<T>);
            }
            Node<T> data = _top.Data;
            _top = _top.Next;
            _count--;
            return data;
        }

        public void Push(Node<T> item)
        {
            ExNode<T> newItem = new ExNode<T>(item);
            if (_top == null)
            {
                _top = newItem;
            }
            else
            {
                newItem.Next = _top;
                _top = newItem;
            }
            _count++;
        }
        public static void ShowList(ExNode<T> top)
        {
            if (top == null)
            {
                return;
            }
            while (top != null)
            {
                Console.WriteLine(top.Data.Data);
                top = top.Next;
            }
        }
    }
    class LinkQueue<T>
    {
        private ExNode<T> _front;
        private ExNode<T> _rear;
        private int _count;
        public int Count { get { return _count; } }
        public ExNode<T> Front { get { return _front; } set { _front = value; } }
        public ExNode<T> Rear { get { return _rear; } set { _rear = value; } }
        public LinkQueue()
        {
            _front = null;
            _rear = null;
        }
        public void Clear()
        {
            _front = null;
            _rear = null;
            _count = 0;
        }
        public Node<T> GetFront()
        {
            if (IsEmpty())
            {
                return default(Node<T>);
            }
            return _front.Data;
        }
        public int GetLength()
        {
            return _count;
        }
        public void In(Node<T> item)
        {
            ExNode<T> newItem = new ExNode<T>(item);
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
        public Node<T> Out()
        {
            if (IsEmpty())
            {
                return default(Node<T>);
            }
            Node<T> temp = _front.Data;
            _front = _front.Next;
            _count--;
            if (_count == 0)
            {
                _front = null;
                _rear = null;
            }
            return temp;
        }
        public static void ShowList(LinkQueue<T> list)
        {
            ExNode<T> tempNode = list.Front;
            while (tempNode != null)
            {
                Console.WriteLine(tempNode.Data);
                tempNode = tempNode.Next;
            }
        }
    }
    class BinaryTree<T> : ITree<T>
    {
        private Node<T> _head;
        public Node<T> Head { get { return _head; } set { _head = value; } }
        public BinaryTree()
        {
            _head = null;
        }
        public BinaryTree(T data)
        {
            Node<T> newHead = new Node<T>(data);
            _head = newHead;
        }
        public BinaryTree(T data, Node<T> leftchild, Node<T> rightchild)
        {
            Node<T> newHead = new Node<T>(data, leftchild, rightchild);
            _head = newHead;
        }
        /// <summary>
        /// 在非递归版本中的几个要点： 
        ///我们需要理解三种遍历方法的思想（以跟节点为基准）； 
        ///所有的节点都可看做是父节点(叶子节点可看做是两个孩子为空的父节点)； 
        ///利用堆栈来保存中间值，来实现非递归版本。
        /// </summary>
        /// <param name="root"></param>
        public static void PreOrder(Node<T> root)
        {
            if (root == null)
            {
                return;
            }
            Console.WriteLine($"{root.Data}");
            PreOrder(root.LeftChild);
            PreOrder(root.RightChild);

        }
        public static void PreOrderNoRecursion(Node<T> root)
        {
            LinkStack<T> myLinkStack = new LinkStack<T>();
            if (root == null)
            {
                return;
            }
            while (!myLinkStack.IsEmpty() || root != null)
            {
                while (root != null)
                {
                    Console.WriteLine(root.Data);
                    myLinkStack.Push(root);
                    root = root.LeftChild;
                }
                if (!myLinkStack.IsEmpty())
                {
                    root = myLinkStack.GetTop();
                    myLinkStack.Pop();
                    root = root.RightChild;
                }
            }
        }
        public static void InOrder(Node<T> root)
        {
            if (root == null)
            {
                return;
            }
            InOrder(root.LeftChild);
            Console.WriteLine($"{root.Data}");
            InOrder(root.RightChild);
        }
        public static void InorderNoRecursion(Node<T> root)
        {
            LinkStack<T> mylinkstack = new LinkStack<T>();
            if (root == null)
            {
                return;
            }
            while (!mylinkstack.IsEmpty() || root != null)
            {
                while (root != null)
                {
                    mylinkstack.Push(root);
                    root = root.LeftChild;
                }
                if (!mylinkstack.IsEmpty())                                 
                {
                    root = mylinkstack.GetTop();
                    mylinkstack.Pop();
                    Console.WriteLine(root.Data);
                    root = root.RightChild;
                }
            }
        }
        public static void PostOrder(Node<T> root)
        {
            if (root == null)
            {
                return;
            }
            PostOrder(root.LeftChild);
            PostOrder(root.RightChild);
            Console.WriteLine($"{root.Data}");
        }
        public static void PostOrderNoRecursion(Node<T> root)
        {
            LinkStack<T> myLinkStack = new LinkStack<T>();
            Node<T> LastVisitMark = null;
            while (root != null)
            {
                myLinkStack.Push(root);
                root = root.LeftChild;
            }
            while (!myLinkStack.IsEmpty())
            {
                root = myLinkStack.GetTop();
                myLinkStack.Pop();
                if (root.RightChild == null || root.RightChild == LastVisitMark)
                {
                    Console.WriteLine(root.Data);
                    LastVisitMark = root;
                }
                else
                {
                    myLinkStack.Push(root);
                    root = root.RightChild;
                    while (root != null)
                    {
                        myLinkStack.Push(root);
                        root = root.LeftChild;
                    }
                }
            }
        }
        public static void MirrorTree(Node<T> root)
        {
            if (root == null)
            {
                return;
            }
            if (root.LeftChild == null && root.RightChild == null)
            {
                return;
            }
            Node<T> tempNode = null;
            if (root.LeftChild != null && root.RightChild != null)
            {
                tempNode = root.LeftChild;
                tempNode.LeftChild = root.RightChild;
                root.RightChild = tempNode;
                MirrorTree(root.LeftChild);
                MirrorTree(root.RightChild);
            }
            else if(root.LeftChild != null)
            {
                root.RightChild = root.LeftChild;
                root.LeftChild = null;
                MirrorTree(root.RightChild);
            }
            else
            {
                root.LeftChild = root.RightChild;
                root.RightChild = null;
                MirrorTree(root.LeftChild);
            }
        }
        /// <summary>
        /// 层次遍历 同时计算树的深度
        /// </summary>
        /// <param name="root"></param>
        public static void LevelOrder(Node<T> root)
        {
            if (root == null)
            {
                return;
            }
            LinkQueue<T> myQueue = new LinkQueue<T>();
            myQueue.In(root);
            int treeHeight = 0;
            int leafnodeCount = 0;
            int allNodeCount = 0;
            bool flag1 = true,flag2 = true;
            Node<T> recordNode = null;
            while (!myQueue.IsEmpty())
            {
                treeHeight++;
                int len = myQueue.GetLength();
                while (len != 0)
                {
                    len--;
                    flag1 = true;
                    flag2 = true;
                    Node<T> tempNode = myQueue.Out();
                    allNodeCount++;
                    Console.WriteLine($"{tempNode.Data}");
                    if (tempNode.LeftChild != null)
                    {
                        myQueue.In(tempNode.LeftChild);
                        flag1 = false;
                    }
                    if (tempNode.RightChild != null)
                    {
                        myQueue.In(tempNode.RightChild);
                        flag1 = false;
                    }
                    recordNode = tempNode.RightChild;
                    if (flag1 && flag2)
                    {
                        leafnodeCount++;
                    }
                }
            }
            Console.WriteLine($"高度为:{treeHeight},总结点数为:{allNodeCount},叶子节点个数为:{leafnodeCount}");
            if (((allNodeCount % 2 == 0) && (recordNode != null)) || ((allNodeCount % 2 == 1) && (recordNode == null)))
            {
                Console.WriteLine("完全二叉树");
            }
            else
            {
                Console.WriteLine("非完全二叉树");
            }
        }
        /// <summary>
        /// 如何快速判断是否为完全二叉树 有右必有左
        /// </summary>
        /// <param name="root"></param>
        public static void IsCompleteBinaryTree(Node<T> root)
        {
            if (root == null)
            {
                return;
            }
            LinkQueue<T> myQueue = new LinkQueue<T>();
            for (int i = 0; i < 6; i++)
            {
                myQueue.In(null);
            }
            Console.WriteLine("*****************");
            Console.WriteLine($"空队列长度为：{myQueue.GetLength()}");
            LinkQueue<T>.ShowList(myQueue);
            myQueue.Clear();
            myQueue.In(root);
            Node<T> tempNode = null;
            while ((tempNode = myQueue.Out()) != null)
            {
                myQueue.In(tempNode.LeftChild);
                myQueue.In(tempNode.RightChild);
            }
            while (!myQueue.IsEmpty())
            {
                Console.WriteLine("");
                if ((tempNode = myQueue.Out()) != null)
                {
                    Console.WriteLine("非完全");
                    return;
                }
            }
            Console.WriteLine("完全");
        }
        public int GetDeepthWithRecursion(Node<T> head)
        {
            if (head == null)
            {
                return 0;
            }
            else if(head.LeftChild == null && head.RightChild == null)
            {
                return 1;
            }
            else
            {
                int leftdeepth = GetDeepthWithRecursion(head.LeftChild);
                int rightdeepth = GetDeepthWithRecursion(head.RightChild);
                return 1 + (leftdeepth > rightdeepth ? leftdeepth : rightdeepth);
            }
        }
        public bool IsEmpty()
        {
            if (_head == null)
            {
                return true;
            }
            return false;
        }

        public Node<T> Root()
        {
            return _head;
        }
        public Node<T> GetLeftChild(Node<T> node)
        {
            return node.LeftChild;
        }
        public Node<T> GetRightChild(Node<T> node)
        {
            return node.RightChild;
        }
        public void InsertLeftChild(T data, Node<T> node)
        {
            Node<T> newNode = new Node<T>(data);
            newNode.LeftChild = node.LeftChild;
            node.LeftChild = newNode;
        }
        public void InsertRightChild(T data, Node<T> node)
        {
            Node<T> newNode = new Node<T>(data);
            newNode.RightChild = node.RightChild;
            node.RightChild = newNode;
        }
        public Node<T> DeleteLeftChild(Node<T> node)
        {
            if (node == null || node.LeftChild == null)
            {
                return null;
            }
            Node<T> tempNode = node.LeftChild;
            node.LeftChild = null;
            return tempNode;

        }
        public Node<T> DeleteRightChild(Node<T> node)
        {
            if (node == null || node.RightChild == null)
            {
                return null;
            }
            Node<T> tempNode = node.RightChild;
            node.RightChild = null;
            return tempNode;
        }
        public bool IsLeafNode(Node<T> node)
        {
            if (node != null && node.LeftChild == null && node.RightChild == null)
            {
                return true;
            }
            return false;
        }
    }
    #region 双亲表示法
    class ParentsdDscribingNode<T>
    {
        private T _data;
        private int _parentIndex;
        public T Data { get { return _data; } set { _data = value; } }
        public int ParentIndex { get { return _parentIndex; } set { _parentIndex = value; } }
        public ParentsdDscribingNode(T data, int index)
        {
            _data = data;
            _parentIndex = index;
        }
    }
    class ParentsDescribingTree<T>
    {
        private static int _maxSize;
        //private ParentsdDscribingNode<T>[] myTreeArray = null;
        private List<ParentsdDscribingNode<T>> myTreeList = new List<ParentsdDscribingNode<T>>();
        public ParentsDescribingTree(int num)
        {
            _maxSize = num;
        }
        public void Add(T data, int nodeIndex, int parentIndex)
        {
            ParentsdDscribingNode<T> tempNode = new ParentsdDscribingNode<T>(data, parentIndex);
            myTreeList.Insert(nodeIndex, tempNode);
        }
        public void ShowParentNode(T data)
        {
            for (int i = 0; i < _maxSize; i++)
            {
                if (myTreeList[i].Data.Equals(data))
                {
                    if (myTreeList[i].ParentIndex == -1)
                    {
                        Console.WriteLine($"{data}为根节点");
                        return;
                    }
                    Console.WriteLine($"node:{data}->ParentNode:{myTreeList[myTreeList[i].ParentIndex].Data}");
                    return;
                }
            }
        }

    }
    #endregion
    #region 孩子双亲表示法
    class ChildNode<T>
    {
        private T _data;
        private ChildNode<T> _next;
        public T Data { get { return _data; } set { _data = Data; } }
        public ChildNode<T> Next { get { return _next; } set { _next = value; } }
        public ChildNode()
        {
            _data = default(T);
            _next = null;
        }
        public ChildNode(T data)
        {
            _data = data;
            _next = null;
        }
    }
    class ChildDescribibgNode<T>
    {
        private T _data;
        private int _parentIndex;
        private ChildNode<T> _childLinkList;
        public T Data { get { return _data; } set { _data = value; } }
        public int ParentIndex { get { return _parentIndex; } set { _parentIndex = value; } }
        public ChildNode<T> ChildLinkList { get { return _childLinkList; } set { _childLinkList = value; } }
        public ChildDescribibgNode(T data, int parentIndex)
        {
            _data = data;
            _parentIndex = parentIndex;
            _childLinkList = null;
        }
    }
    class ChildDescribingTree<T>
    {
        private int _maxSize;
        private List<ChildDescribibgNode<T>> myChildTree = new List<ChildDescribibgNode<T>>();
        public ChildDescribingTree(int num)
        {
            _maxSize = num;
        }
        public void Add(T data, int nodeIndex, int parentIndex)
        {
            ChildDescribibgNode<T> tempNode = new ChildDescribibgNode<T>(data, parentIndex);
            myChildTree.Insert(nodeIndex, tempNode);
        }
        public void AddChildLinkList(T data, T child)
        {
            ChildNode<T> tempChildNode = new ChildNode<T>(child);
            for (int i = 0; i < myChildTree.Count; i++)
            {
                if (myChildTree[i].Data.Equals(data))
                {
                    ChildNode<T> tempNode = myChildTree[i].ChildLinkList;
                    if (tempNode == null)
                    {
                        myChildTree[i].ChildLinkList = tempChildNode;
                    }
                    else
                    {
                        while (tempNode.Next != null)
                        {
                            tempNode = tempNode.Next;
                        }
                        tempNode.Next = tempChildNode;
                    }
                    return;
                }
            }
        }
        public void ShowChildParent(T data)
        {
            for (int i = 0; i < myChildTree.Count; i++)
            {
                if (myChildTree[i].Data.Equals(data))
                {
                    if (myChildTree[i].ParentIndex == -1)
                    {
                        Console.WriteLine($"{data}为根节点");
                    }
                    else
                    {
                        Console.WriteLine($"{data}的父节点为{myChildTree[myChildTree[i].ParentIndex].Data}");
                    }
                    ChildNode<T> tempNode = myChildTree[i].ChildLinkList;
                    if (tempNode != null)
                    {
                        Console.WriteLine($"{data}的子节点为");
                        while (tempNode != null)
                        {
                            Console.Write($"{tempNode.Data}->");
                            tempNode = tempNode.Next;
                        }
                        Console.Write("null\n");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"{data}没有孩子节点");
                    }
                }
            }
        }
    }
    #endregion
    #region 孩子兄弟表示法
    class ChildBrotherNode<T>:ICloneable
    {
        private T _data;
        private ChildBrotherNode<T> _childNode;
        private ChildBrotherNode<T> _brotherNode;
        public T Data { get { return _data; } set { _data = value; } }
        public ChildBrotherNode<T> ChildNode { get { return _childNode; } set { _childNode = value; } }
        public ChildBrotherNode<T> BrotherNode { get { return _brotherNode; } set { _brotherNode = value; } }
        //public ChildBrotherNode<T> CloneNode = null;
        public ChildBrotherNode()
        {
            _data = default(T);
            _childNode = null;
            _brotherNode = null;
        }
        public ChildBrotherNode(T item)
        {
            _data = item;
            _childNode = null;
            _brotherNode = null;
        }
        public ChildBrotherNode(T item, ChildBrotherNode<T> childnode, ChildBrotherNode<T> brothernode)
        {
            _data = item;
            _childNode = childnode;
            _brotherNode = brothernode;
        }
        public ChildBrotherNode<T> ShallowCopy()
        {
            return (ChildBrotherNode<T>)this.MemberwiseClone();
        }
        /// <summary>
        /// 下属两种为不同的深拷贝方式
        /// </summary>
        /// <returns></returns>
        public ChildBrotherNode<T> DeepCopy()
        {
            ChildBrotherNode<T> tempNode = (ChildBrotherNode<T>)this.MemberwiseClone();
            tempNode = new ChildBrotherNode<T>(_data,_childNode,_brotherNode);
            return tempNode;
        }
        public object Clone()
        {
            return this as object;
        }
    }
    class ChildBrotherExNode<T>
    {
        private ChildBrotherNode<T> _data;
        private ChildBrotherExNode<T> _next;
        public ChildBrotherNode<T> Data { get { return _data; } set { _data = value; } }
        public ChildBrotherExNode<T> Next { get { return _next; } set { _next = value; } }

        public ChildBrotherExNode()
        {
            _data = null;
            _next = null;
        }
        public ChildBrotherExNode(ChildBrotherNode<T> item)
        {
            _data = item;
            _next = null;
        }
    }
    class ChildBritherLinkStack<T>
    {
        private ChildBrotherExNode<T> _top;
        private int _count;
        public ChildBrotherExNode<T> Top { get { return _top; } set { _top = value; } }
        public int Count { get { return _count; } }
        public ChildBritherLinkStack()
        {
            _top = null;
            _count = 0;
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

        public ChildBrotherNode<T> GetTop()
        {
            if (IsEmpty())
            {
                return default(ChildBrotherNode<T>);
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

        public ChildBrotherNode<T> Pop()
        {
            if (IsEmpty())
            {
                return default(ChildBrotherNode<T>);
            }
           ChildBrotherNode<T> data = _top.Data;
            _top = _top.Next;
            _count--;
            return data;
        }

        public void Push(ChildBrotherNode<T> item)
        {
            ChildBrotherExNode<T> newItem = new ChildBrotherExNode<T>(item);
            if (_top == null)
            {
                _top = newItem;
            }
            else
            {
                newItem.Next = _top;
                _top = newItem;
            }
            _count++;
        }
        public static void ShowList(ChildBrotherExNode<T> top)
        {
            if (top == null)
            {
                return;
            }
            while (top != null)
            {
                Console.WriteLine(top.Data.Data);
                top = top.Next;
            }
        }
    }
    class ChildBrotherDescribingTree<T> 
    {
        private ChildBrotherNode<T> _head;
        public ChildBrotherNode<T> Head { get { return _head; } set { _head = value; } }
        public ChildBrotherDescribingTree()
        {
            _head = null;
        }
        public ChildBrotherDescribingTree(T item)
        {
            _head = new ChildBrotherNode<T>(item);
        }
        public static void ExchangeLeftRight(ChildBrotherNode<T> root)
        {
            if (root == null)
            {
                return;
            }
            ChildBrotherNode<T> data = null;
            if (root.ChildNode != null && root.BrotherNode == null)
            {
                //root.BrotherNode = root.ChildNode.Clone() as ChildBrotherNode<T>; 
                //root.ChildNode = null;
                root.BrotherNode = root.ChildNode.DeepCopy();
                root.ChildNode = null;
            }
            else if(root.ChildNode == null && root.BrotherNode != null)
            {
                //root.ChildNode = root.BrotherNode.Clone() as ChildBrotherNode<T>;
                //root.BrotherNode = null;
                root.ChildNode = root.BrotherNode.DeepCopy();
                root.BrotherNode = null;
            }
            else if(root.ChildNode != null && root.BrotherNode != null)  
            {
                //data = root.ChildNode.Clone() as ChildBrotherNode<T>;
                //root.ChildNode = root.BrotherNode.Clone() as ChildBrotherNode<T>;
                //root.BrotherNode = data.Clone() as ChildBrotherNode<T>;
                data = root.ChildNode.DeepCopy();
                root.ChildNode = root.BrotherNode.DeepCopy();
                root.BrotherNode = data.DeepCopy();
            }
            //data = root.ChildNode.Clone() as ChildBrotherNode<T>;
            //root.ChildNode = root.BrotherNode.Clone() as ChildBrotherNode<T>;
            //root.BrotherNode = data.Clone() as ChildBrotherNode<T>;
            ExchangeLeftRight(root.ChildNode);
            ExchangeLeftRight(root.BrotherNode);
        }
        public static void PreOrderNoRecursion(ChildBrotherNode<T> root,T data)
        {
            ChildBritherLinkStack<T> myLinkStack = new ChildBritherLinkStack<T>();
            if (root == null)
            {
                return;
            }
            while (!myLinkStack.IsEmpty() || root != null)
            {
                while (root != null)
                {
                    if (root.Data.Equals(data))
                    {
                        if (root.ChildNode != null)
                        {
                            Console.WriteLine($"{data}的孩子节点为:{root.ChildNode.Data}");
                        }
                        else
                        {
                            Console.WriteLine($"{data}的没有孩子节点");
                        }
                        if (root.BrotherNode != null)
                        {
                            Console.WriteLine($"{data}的兄弟节点为:{root.BrotherNode.Data}");
                        }
                        else
                        {
                            Console.WriteLine($"{data}的没有兄弟节点");
                        }
                        return;
                    }
                    myLinkStack.Push(root);
                    root = root.ChildNode;
                }
                if (!myLinkStack.IsEmpty())
                {
                    root = myLinkStack.GetTop();
                    myLinkStack.Pop();
                    root = root.BrotherNode;
                }
            }

        }

    }
    #endregion

}
