using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackAndQueue;
using SeqList;
namespace TreeAndGraph
{
    class Program
    {
        #region 树的测试
        static void TestChildBrother()
        {
            ChildBrotherDescribingTree<char> myChildBrotherTree = new ChildBrotherDescribingTree<char>();
            List<ChildBrotherNode<char>> mylist = new List<ChildBrotherNode<char>>();
            for (char i = 'A'; i < 'K'; i++)
            {
                mylist.Add(new ChildBrotherNode<char>(i));
            }
            mylist.Insert(0, new ChildBrotherNode<char>('R'));
            mylist[0].ChildNode = mylist[1];
            mylist[1].BrotherNode = mylist[2];
            mylist[1].ChildNode = mylist[4];
            mylist[2].BrotherNode = mylist[3];
            mylist[4].BrotherNode = mylist[5];
            mylist[3].ChildNode = mylist[6];
            mylist[6].ChildNode = mylist[7];
            mylist[7].BrotherNode = mylist[8];
            mylist[8].BrotherNode = mylist[9];
            List<char> charList11 = new List<char>();
            for (char i = 'A'; i <= 'I'; i++)
            {
                charList11.Add(i);
            }
            charList11.Insert(0, 'R');
            for (int i = 0; i < charList11.Count; i++)
            {
                ChildBrotherDescribingTree<char>.PreOrderNoRecursion(mylist[0], charList11[i]);
            }
            ChildBrotherDescribingTree<char>.ExchangeLeftRight(mylist[0]);
            Console.WriteLine("交换后");
            for (int i = 0; i < charList11.Count; i++)
            {
                ChildBrotherDescribingTree<char>.PreOrderNoRecursion(mylist[0], charList11[i]);
            }

        }
        static void TestChildDescribingTree()
        {
            ChildDescribingTree<char> myChildTree = new ChildDescribingTree<char>(9);
            myChildTree.Add('R', 0, -1);
            myChildTree.Add('A', 1, 0);
            myChildTree.Add('B', 2, 0);
            myChildTree.Add('C', 3, 0);
            myChildTree.Add('D', 4, 1);
            myChildTree.Add('E', 5, 1);
            myChildTree.Add('F', 6, 3);
            myChildTree.Add('G', 7, 6);
            myChildTree.Add('H', 8, 6);
            myChildTree.Add('K', 9, 6);
            myChildTree.Add('I', 10, 6);

            myChildTree.AddChildLinkList('R', 'A');
            myChildTree.AddChildLinkList('R', 'B');
            myChildTree.AddChildLinkList('R', 'C');
            myChildTree.AddChildLinkList('A', 'D');
            myChildTree.AddChildLinkList('A', 'E');
            myChildTree.AddChildLinkList('C', 'F');
            myChildTree.AddChildLinkList('F', 'G');
            myChildTree.AddChildLinkList('F', 'H');
            myChildTree.AddChildLinkList('F', 'K');
            myChildTree.AddChildLinkList('F', 'I');
            List<char> charList1 = new List<char>();
            for (char i = 'A'; i <= 'I'; i++)
            {
                charList1.Add(i);
            }
            charList1.Insert(0, 'R');
            for (int i = 0; i < charList1.Count; i++)
            {
                myChildTree.ShowChildParent(charList1[i]);
            }
            myChildTree.ShowChildParent('F');
        }
        static void TestParentDescribing()
        {
            ParentsDescribingTree<char> myTree = new ParentsDescribingTree<char>(9);
            myTree.Add('A', 0, -1);
            char temp = 'B';
            for (int i = 1; i < 9; i++, temp++)
            {
                myTree.Add(temp, i, (i - 1) / 2);
            }
            List<char> charList = new List<char>();
            for (char i = 'A'; i <= 'I'; i++)
            {
                charList.Add(i);
            }
            for (int i = 0; i < charList.Count; i++)
            {
                myTree.ShowParentNode(charList[i]);
            }
        }
        static void TestBinaryTree()
        {
            List<Node<char>> charNodeList = new List<Node<char>>();
            List<char> charArray = new List<char>();
            for (char cha = 'A'; cha <= 'O'; cha++)
            {
                charArray.Add(cha);
            }
            foreach (var item in charArray)
            {
                Node<char> tempNode = new Node<char>(item);
                charNodeList.Add(tempNode);
            }
            charNodeList[0].LeftChild = charNodeList[1];
            charNodeList[0].RightChild = charNodeList[2];
            charNodeList[1].LeftChild = charNodeList[3];
            charNodeList[1].RightChild = charNodeList[4];
            charNodeList[2].LeftChild = charNodeList[5];
            charNodeList[2].RightChild = charNodeList[6];
            //charNodeList[3].LeftChild = charNodeList[7];
            //charNodeList[3].RightChild = charNodeList[8];
            //charNodeList[4].RightChild = charNodeList[9];
            //charNodeList[5].LeftChild = charNodeList[10];
            //charNodeList[5].RightChild = charNodeList[14];
            //charNodeList[9].LeftChild = charNodeList[11];
            //charNodeList[9].RightChild = charNodeList[12];
            //charNodeList[12].RightChild = charNodeList[13];
            BinaryTree<char> myHead = new BinaryTree<char>();
            myHead.Head = charNodeList[0];
            Console.WriteLine("翻转前");
            Console.WriteLine("层序遍历");
            BinaryTree<char>.LevelOrder(myHead.Head);
            Console.WriteLine("翻转后");
            BinaryTree<char>.MirrorTree(myHead.Head);
            Console.WriteLine("层序遍历");
            BinaryTree<char>.LevelOrder(myHead.Head);


            Console.WriteLine($"树的深度为:{myHead.GetDeepthWithRecursion(myHead.Head)}");
            BinaryTree<char>.IsCompleteBinaryTree(myHead.Head);


            Console.WriteLine("后序遍历");
            BinaryTree<char>.PostOrder(myHead.Head);
            Console.WriteLine("非递归后序遍历");
            BinaryTree<char>.PostOrderNoRecursion(myHead.Head);
            Console.WriteLine("前序遍历");
            BinaryTree<char>.PreOrder(myHead.Head);
            Console.WriteLine("非递归前序遍历");
            BinaryTree<char>.PreOrderNoRecursion(myHead.Head);

            Console.WriteLine("非递归中序遍历");
            BinaryTree<char>.InorderNoRecursion(myHead.Head);
            Console.WriteLine("中序遍历");
            BinaryTree<char>.InOrder(myHead.Head);


            Console.WriteLine("层序遍历");
            BinaryTree<char>.LevelOrder(myHead.Head);
            Console.WriteLine("前序遍历");
            BinaryTree<char>.PreOrder(myHead.Head);
            Console.WriteLine("中序遍历");
            BinaryTree<char>.InOrder(myHead.Head);
            Console.WriteLine("后序遍历");
            BinaryTree<char>.PostOrder(myHead.Head);
        }
        #endregion
        #region 图的邻接矩阵表示 BFS遍历  
        static StackAndQueue.LinkQueue<GraphNode<T>> BFSGetNeighbor<T>(GraphAdjMatrix<T> graph, GraphNode<T> node)
        {
            StackAndQueue.LinkQueue<GraphNode<T>> myQueue = new StackAndQueue.LinkQueue<GraphNode<T>>();
            foreach (var item in graph.Nodes)
            {
                if (graph.IsEdge(item, node))
                {
                    myQueue.In(item);
                }
            }
            return myQueue;
        }
        static void BFS<T>(GraphAdjMatrix<T> myGraph)
        {
            StackAndQueue.LinkQueue<GraphNode<T>> myQueue = new StackAndQueue.LinkQueue<GraphNode<T>>();
            StackAndQueue.LinkQueue<GraphNode<T>> tempQueue = null;
            bool[] FlagVIsit = new bool[myGraph.Nodes.Length];
            GraphNode<T> tempIndex = null;
            GraphNode<T> tempNode = null;
            for (int i = 0; i < myGraph.Nodes.Length; i++)
            {
                FlagVIsit[i] = false;
            }
            for (int i = 0; i < myGraph.Nodes.Length; i++)
            {
                if (FlagVIsit[i])
                {
                    continue;
                }
                else
                {
                    myQueue.In(myGraph.Nodes[i]);
                    while (!myQueue.IsEmpty())
                    {
                        tempIndex = myQueue.Out();
                        Console.Write(tempIndex.Data.ToString() + "->");
                        FlagVIsit[myGraph.GetIndex(tempIndex)] = true;
                        tempQueue = BFSGetNeighbor<T>(myGraph, tempIndex);
                        while (!tempQueue.IsEmpty())
                        {
                            tempNode = tempQueue.Out();
                            if (!FlagVIsit[myGraph.GetIndex(tempNode)])
                            {
                                FlagVIsit[myGraph.GetIndex(tempNode)] = true;
                                myQueue.In(tempNode);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("null\n");
        }
        #endregion

        #region 图的邻接链表表示 BFS遍历
        static void BFSInLink(ref bool[] FlagVIsit, List<HeadLinkList<int>> list)
        {
            StackAndQueue.LinkQueue<int> myQueue = new StackAndQueue.LinkQueue<int>();
            HeadLinkList<int> tempLink = new HeadLinkList<int>();
            int tempIndex = default(int);
            SeqList.Node<int> tempNode = null;
            SeqList.Node<int> tempNode1 = null;
            foreach (var item in list)
            {
                tempNode = item.Head.Next;
                if (!FlagVIsit[tempNode.Data])
                {
                    myQueue.In(tempNode.Data);
                    while (!myQueue.IsEmpty())
                    {
                        tempIndex = myQueue.Out();
                        Console.Write(tempIndex.ToString() + "->");
                        tempLink = list[tempIndex];
                        FlagVIsit[tempIndex] = true;
                        tempNode = tempLink.Head.Next;
                        while (tempNode.Next != null)
                        {
                            tempNode = tempNode.Next;
                            if (!FlagVIsit[tempNode.Data])
                            {
                                FlagVIsit[tempNode.Data] = true;
                                myQueue.In(tempNode.Data);
                            }
                        }
                    }
                }
            }
        }
        #endregion


        static void DFSRecursionSearch<T>(ref bool[] FlagVIsit, GraphNode<T> node, GraphAdjMatrix<T> myGraph, StackAndQueue.LinkQueue<GraphNode<T>> myQueue)
        {
            FlagVIsit[myGraph.GetIndex(node)] = true;
            Console.Write(node.Data + "->");
            GraphNode<T> tempNode = null;
            while (!myQueue.IsEmpty())
            {
                tempNode = myQueue.Out();
                if (!FlagVIsit[myGraph.GetIndex(tempNode)])
                {
                    DFSRecursionSearch<T>(ref FlagVIsit, tempNode, myGraph, BFSGetNeighbor<T>(myGraph, tempNode));
                }
            }

        }
        static void DFS<T>(ref bool[] FlagVIsit, GraphAdjMatrix<T> myGraph)
        {
            for (int i = 0; i < myGraph.Nodes.Length; i++)
            {
                if (FlagVIsit[i])
                {
                    continue;
                }
                else
                {
                    DFSRecursionSearch<T>(ref FlagVIsit, myGraph.Nodes[i], myGraph, BFSGetNeighbor<T>(myGraph, myGraph.Nodes[i]));
                }
            }
            Console.WriteLine("null\n");
        }

        static int GetItemInde(List<HeadLinkList<int>> list, SeqList.Node<int> tempNode)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Head.Next.Data.Equals(tempNode.Data))
                {
                    return i;
                }
            }
            return -1;
        }
        static void DFSRecursionSearchInLink(ref bool[] FlagVIsit, List<HeadLinkList<int>> list, HeadLinkList<int> item)
        {
            SeqList.Node<int> tempNode = item.Head.Next;
            FlagVIsit[tempNode.Data] = true;
            Console.Write($"{tempNode.Data}->");
            while (tempNode.Next != null)
            {
                if (!FlagVIsit[tempNode.Next.Data])
                {
                    DFSRecursionSearchInLink(ref FlagVIsit, list, list[GetItemInde(list, tempNode.Next)]);
                }
                else
                {
                    tempNode = tempNode.Next;
                }
            }

        }
        static void DFSInLink(ref bool[] FlagVIsit, List<HeadLinkList<int>> list)
        {
            foreach (var item in list)
            {
                if (!FlagVIsit[item.Head.Next.Data])
                {
                    DFSRecursionSearchInLink(ref FlagVIsit, list, item);
                }
            }
        }

        static LinkGraphNode<T> GetNodeIndex<T>(List<LinkGraphList<T>> list, LinkGraphNode<T> node)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Tail.Next.Data.Equals(node.Data))
                {
                    return list[i].Tail.Next;
                }
            }
            return null;
        }
       
        /// <summary>
        /// 如何快速写出邻接表的代码，基于邻接矩阵的分析，转化的思想
        /// 熟能生巧，将思想转化为心智的本能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        static void Prim<T>(List<LinkGraphList<T>> list,int index)
        {
            Dictionary<T, bool> FlagVisit = new Dictionary<T, bool>();
            List<LinkGraphNode<T>> tempNodeList = new List<LinkGraphNode<T>>();
            LinkGraphNode<T> tempNode = null;
            LinkGraphNode<T> MarkNode = null;
            double distance = double.MaxValue;
            foreach (var item in list)
            {
                FlagVisit[item.Tail.Next.Data] = false;
            }
            tempNodeList.Add(list[index].Tail.Next);
            FlagVisit[list[index].Tail.Next.Data] = true;
            while (tempNodeList.Count > 0 && tempNodeList.Count < list.Count)
            {
                distance = double.MaxValue;
                foreach (var item in tempNodeList)
                {
                    tempNode = item;
                    while (tempNode.Next != null)
                    {
                        tempNode = tempNode.Next;
                        if (!FlagVisit[tempNode.Data])
                        {
                            if (tempNode.Distance <= distance)
                            {
                                MarkNode = tempNode;
                                distance = tempNode.Distance;
                            }
                        }
                    }
                }
                tempNodeList.Add(GetNodeIndex<T>(list,MarkNode));
                FlagVisit[MarkNode.Data] = true;
            }
            foreach (var item in tempNodeList)
            {
                Console.Write($"{item.Data}->");
            }
            Console.WriteLine("null\n");
        }

        static int GetMinDisIndex(int[] dist,bool[]flag)
        {
            int std = int.MaxValue;
            int index = 0;
            for (int i = 0; i < dist.Length; i++)
            {
                if (!flag[i] && dist[i] < std)
                {
                    std = dist[i];
                    index = i;
                }
            }
            return index;
        }
        static void Dijkstra(int index,int[,]matrix)
        {
            int len = matrix.GetLength(0);
            bool[] flag = new bool[len];
            for (int i = 0; i < len; i++)
            {
                flag[i] = false;
            }
            int[] DistanceMatrix = new int[len];
            for (int i = 0; i < len; i++)
            {
                DistanceMatrix[i] = int.MaxValue;
            }
            DistanceMatrix[index] = 0;
            flag[index] = true;
            for (int i = 0; i < len - 1; i++)
            {
                int tempindex = GetMinDisIndex(DistanceMatrix, flag);
                flag[tempindex] = true;
                for (int j = 0; j < len; j++)
                {
                    if (!flag[j] && matrix[tempindex,j] != 0 && DistanceMatrix[tempindex] + matrix[tempindex, j] < DistanceMatrix[j])
                    {
                        DistanceMatrix[j] = DistanceMatrix[tempindex] + matrix[tempindex, j];
                    }
                }

            }
            for (int i = 0; i < len; i++)
            {
                Console.WriteLine($"{index}->{i}:{DistanceMatrix[i]}");
            }


        }
        static void Main(string[] args)
        {
            int[,] mymatrix = { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
                                      { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
                                      { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
                                      { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
                                      { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
                                      { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
                                      { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
                                      { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
                                      { 0, 0, 2, 0, 0, 0, 6, 7, 0 } };
            Dijkstra(0, mymatrix);



            List<LinkGraphList<int>> myLinkGraphList = new List<LinkGraphList<int>>();
            const int Count = 7;
            for (int i = 0; i < Count; i++)
            {
                LinkGraphList<int> tempLinkGraph = new LinkGraphList<int>();
                tempLinkGraph.AddElement(i, 0);
                myLinkGraphList.Add(tempLinkGraph);
            }
            myLinkGraphList[0].AddElement(1, 6);
            myLinkGraphList[0].AddElement(2, 1);
            myLinkGraphList[0].AddElement(3, 5);
            myLinkGraphList[1].AddElement(0, 6);
            myLinkGraphList[1].AddElement(2, 5);
            myLinkGraphList[1].AddElement(4, 3);
            myLinkGraphList[2].AddElement(0, 1);
            myLinkGraphList[2].AddElement(1, 5);
            myLinkGraphList[2].AddElement(3, 5);
            myLinkGraphList[2].AddElement(4, 6);
            myLinkGraphList[2].AddElement(5, 4);
            myLinkGraphList[3].AddElement(0, 5);
            myLinkGraphList[3].AddElement(2, 5);
            myLinkGraphList[3].AddElement(5, 2);
            myLinkGraphList[4].AddElement(1, 3);
            myLinkGraphList[4].AddElement(2, 6);
            myLinkGraphList[4].AddElement(5, 6);
            myLinkGraphList[4].AddElement(6, 1);
            myLinkGraphList[5].AddElement(3, 2);
            myLinkGraphList[5].AddElement(2, 4);
            myLinkGraphList[5].AddElement(4, 6);
            foreach (var item in myLinkGraphList)
            {
                item.Show(item);
            }
            Prim<int>(myLinkGraphList,4);


            List<HeadLinkList<int>> mylist = new List<HeadLinkList<int>>();
            bool[] FlagVIsit = new bool[100];
            bool[] FlagVIsit11 = new bool[100];
            bool[] FlagVIsit12 = new bool[100];

            for (int i = 0; i < 11; i++)
            {
                HeadLinkList<int> tempLinkLIst = new HeadLinkList<int>();
                tempLinkLIst.AddElement(i);
                mylist.Add(tempLinkLIst);
            }
            foreach (var item in mylist)
            {
                HeadLinkList<int>.ShowList(item.Head);
            }
            mylist[0].AddElement(1);
            mylist[0].AddElement(2);
            mylist[0].AddElement(3);
            mylist[0].AddElement(4);
            mylist[1].AddElement(0);
            mylist[1].AddElement(4);
            mylist[1].AddElement(7);
            mylist[1].AddElement(9);
            mylist[2].AddElement(0);
            mylist[3].AddElement(0);
            mylist[3].AddElement(5);
            mylist[3].AddElement(6);
            mylist[4].AddElement(0);
            mylist[4].AddElement(1);
            mylist[4].AddElement(5);
            mylist[5].AddElement(3);
            mylist[5].AddElement(4);
            mylist[6].AddElement(3);
            mylist[7].AddElement(1);
            mylist[7].AddElement(8);
            mylist[7].AddElement(10);
            mylist[8].AddElement(7);
            mylist[9].AddElement(1);
            mylist[10].AddElement(7);
            Console.WriteLine("\n");
            foreach (var item in mylist)
            {
                HeadLinkList<int>.ShowList(item.Head);
                Console.WriteLine("\n");
            }
            DFSInLink(ref FlagVIsit, mylist);
            Console.WriteLine("\n");
            BFSInLink(ref FlagVIsit11, mylist);



            const int NodeCount = 11;
            GraphAdjMatrix<int> myGraphInt = new GraphAdjMatrix<int>(NodeCount);
            List<GraphNode<int>> myIntList = new List<GraphNode<int>>();
            for (int i = 0; i < NodeCount; i++)
            {
                myIntList.Add(new GraphNode<int>(i));
                myGraphInt.Nodes[i] = myIntList[i];
            }
            #region 一种图结构
            //myGraphInt.SetEdge(myIntList[0], myIntList[2], 3);
            //myGraphInt.SetEdge(myIntList[1], myIntList[3], 3);
            //myGraphInt.SetEdge(myIntList[1], myIntList[2], 11);
            //myGraphInt.SetEdge(myIntList[1], myIntList[4], 17);
            //myGraphInt.SetEdge(myIntList[2], myIntList[5], 17);
            //myGraphInt.SetEdge(myIntList[3], myIntList[4], 17);
            //myGraphInt.SetEdge(myIntList[3], myIntList[5], 17);
            //myGraphInt.SetEdge(myIntList[3], myIntList[6], 17);
            //myGraphInt.SetEdge(myIntList[4], myIntList[6], 17);
            //myGraphInt.SetEdge(myIntList[5], myIntList[6], 17);
            #endregion
            #region 另一种图结构
            myGraphInt.SetEdge(myIntList[0], myIntList[1], 3);
            myGraphInt.SetEdge(myIntList[0], myIntList[2], 3);
            myGraphInt.SetEdge(myIntList[0], myIntList[3], 3);
            myGraphInt.SetEdge(myIntList[0], myIntList[4], 3);
            myGraphInt.SetEdge(myIntList[1], myIntList[4], 3);
            myGraphInt.SetEdge(myIntList[1], myIntList[7], 11);
            myGraphInt.SetEdge(myIntList[1], myIntList[9], 17);
            myGraphInt.SetEdge(myIntList[3], myIntList[5], 17);
            myGraphInt.SetEdge(myIntList[3], myIntList[6], 17);
            myGraphInt.SetEdge(myIntList[4], myIntList[5], 17);
            myGraphInt.SetEdge(myIntList[7], myIntList[8], 17);
            myGraphInt.SetEdge(myIntList[7], myIntList[10], 3);
            #endregion
            #region 第三种图结构
            //myGraphInt.SetEdge(myIntList[1], myIntList[2], 3);
            //myGraphInt.SetEdge(myIntList[1], myIntList[3], 11);
            //myGraphInt.SetEdge(myIntList[2], myIntList[4], 17);
            //myGraphInt.SetEdge(myIntList[2], myIntList[5], 17);
            //myGraphInt.SetEdge(myIntList[3], myIntList[6], 17);
            //myGraphInt.SetEdge(myIntList[3], myIntList[7], 17);
            //myGraphInt.SetEdge(myIntList[4], myIntList[8], 17);
            //myGraphInt.SetEdge(myIntList[5], myIntList[8], 3);
            //myGraphInt.SetEdge(myIntList[6], myIntList[7], 3);
            #endregion
            Console.WriteLine("\n");
            DFS<int>(ref FlagVIsit12, myGraphInt);
            BFS<int>(myGraphInt);

            GraphAdjMatrix<char> myGraph = new GraphAdjMatrix<char>(10);
            List<GraphNode<char>> myCharList = new List<GraphNode<char>>();
            for (char cha = 'A'; cha <= 'J'; cha++)
            {
                myCharList.Add(new GraphNode<char>(cha));
                myGraph.Nodes[(int)cha - 65] = myCharList[(int)cha - 65];
            }
            foreach (var item in myCharList)
            {
                Console.WriteLine(myGraph.GetIndex(item));
            }
            myGraph.SetEdge(myCharList[0], myCharList[3], 3);
            myGraph.SetEdge(myCharList[3], myCharList[8], 11);
            myGraph.SetEdge(myCharList[8], myCharList[9], 17);
            myGraph.SetEdge(myCharList[1], myCharList[5], 6);
            myGraph.SetEdge(myCharList[5], myCharList[7], 12);
            myGraph.SetEdge(myCharList[2], myCharList[4], 6);
            myGraph.SetEdge(myCharList[4], myCharList[6], 10);
            myGraph.SetEdge(myCharList[3], myCharList[6], 9);
            myGraph.SetEdge(myCharList[6], myCharList[9], 15);
            Console.WriteLine($"长度为:{myGraph.Matrix.GetLength(0)},宽度为:{myGraph.Matrix.GetLength(1)}");
            for (int i = 0; i < myGraph.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < myGraph.Matrix.GetLength(1); j++)
                {
                    Console.Write(myGraph.Matrix[i, j] + "  ");
                }
                Console.WriteLine("\n");
            }
            BFS<char>(myGraph);
            TestChildBrother();
            Console.WriteLine("So Cool!!!");
        }
    }
}
