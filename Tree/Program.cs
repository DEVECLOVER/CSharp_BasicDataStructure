using System;
using Tree;
using TestBinaryTreeNamespace;
namespace TestBinaryTreeNamespace
{
    class TestBinaryTree
    {
        public static void TestTraversal()
        {
            int[] nums = { 1, 3, 5, 7, 9 };
            BinaryTree<int> myTree = new BinaryTree<int>(nums);

            Console.WriteLine("递归前序");
            BinaryTree<int>.GetTreePreOrderByRecursion(myTree.RootNode);
            Console.WriteLine();

            Console.WriteLine("非递归前序");
            BinaryTree<int>.GetTreePreOrderNotRecursion(myTree.RootNode);
            Console.WriteLine();

            Console.WriteLine("递归中序");
            BinaryTree<int>.GetTreeInOrderByRecursion(myTree.RootNode);
            Console.WriteLine();

            Console.WriteLine("非递归中序");
            BinaryTree<int>.GetTreeInOrderNotRecursion(myTree.RootNode);
            Console.WriteLine();

            Console.WriteLine("递归后序");
            BinaryTree<int>.GetTreePostOrderByRecursion(myTree.RootNode);
            Console.WriteLine();

            Console.WriteLine("非递归后序");
            BinaryTree<int>.GetTreePostOrderNotRecursion(myTree.RootNode);
            Console.WriteLine();

            Console.WriteLine("层序遍历");
            BinaryTree<int>.GetTreeLevelOrder(myTree.RootNode);
            Console.WriteLine();
        }
        public static void TestTreeClone()
        {
            int[] nums = { 1, 3, 5, 7, 9};
            BinaryTree<int> myTree = new BinaryTree<int>(nums);
            BinaryTree<int>.GetTreePreOrderByRecursion(myTree.RootNode);
            Console.WriteLine();
            TreeNode<int> BfsClone = BinaryTree<int>.BFSCloneTree(myTree.RootNode);
            BinaryTree<int>.GetTreeInOrderByRecursion(BfsClone);
            Console.WriteLine();
            TreeNode<int> DfsClone = BinaryTree<int>.DFSCloneTree(myTree.RootNode);
            BinaryTree<int>.GetTreePostOrderByRecursion(DfsClone);
            Console.WriteLine();
        }
        public static void TestMirrorTree()
        {
            int[] nums = { 1, 3, 5, 7, 9,0,6};
            BinaryTree<int> myTree = new BinaryTree<int>(nums);
            BinaryTree<int>.GetTreePreOrderNotRecursion(myTree.RootNode);
            Console.WriteLine();
            BinaryTree<int>.MirrorTree(myTree.RootNode);
            BinaryTree<int>.GetTreePreOrderByRecursion(myTree.RootNode);
            Console.WriteLine();
        }
        public static void TestSwap()
        {
            TreeNode<int> p = new TreeNode<int>(3);
            TreeNode<int> q = new TreeNode<int>(6);
            Console.WriteLine(p.Data);
            Console.WriteLine(q.Data);
            BinaryTree<int>.SwapTreeNode(ref p, ref q);
            Console.WriteLine(p.Data);
            Console.WriteLine(q.Data);
        }
        public static void TestTreeHeight()
        {
            int[] nums = { 1, 3, 5, 7, 9, 0 };
            BinaryTree<int> myTree = new BinaryTree<int>(nums);
            int height = BinaryTree<int>.GetTreeHeightByRecursion(myTree.RootNode);
            Console.WriteLine(height);
            height = BinaryTree<int>.GetTreeHeightNotRecursion(myTree.RootNode);
            Console.WriteLine(height);
        }
        public static void TestTreeNodeNum()
        {
            int[] nums = { 1, 3, 5, 7, 9, 0 };
           // nums = new int[]{ 1,3,5,7};
           // nums = new int[] { 1 };
            BinaryTree<int> myTree = new BinaryTree<int>(nums);
            Console.WriteLine("树节点总数");
            Console.WriteLine(BinaryTree<int>.GetTreeNodeNum(myTree.RootNode));
            Console.WriteLine("叶子节点总数");
            Console.WriteLine(BinaryTree<int>.CountLeafTreeNode(myTree.RootNode));

        }
    }
}
namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestBinaryTree.TestTreeClone();
            //TestBinaryTree.TestMirrorTree();
            TestBinaryTree.TestTreeNodeNum();
            TestBinaryTree.TestTraversal();
            Console.Read();
        }
    }
}
