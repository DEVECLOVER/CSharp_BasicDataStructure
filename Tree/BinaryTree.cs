using System;
using StackAndQueue;
namespace Tree
{
    class BinaryTree<T> 
    {
        private TreeNode<T> _rootNode;
        public TreeNode<T> RootNode { get { return  _rootNode; } set { _rootNode = value; } }
        public BinaryTree()
        {
            _rootNode = null;
        }
        public BinaryTree(params T[] nums)
        {
            _rootNode = MakeArray2Tree(0, nums.Length - 1, nums);
        }
        public static TreeNode<T> MakeArray2Tree(int left,int right, params T[] nums)
        {
            if (left > right)
            {
                return null;
            }
            int mid = (left + right) >> 1;
            TreeNode<T> newTreeNode = new TreeNode<T>(nums[mid]);
            newTreeNode.LeftNode = MakeArray2Tree(left, mid - 1, nums);
            newTreeNode.RightNode = MakeArray2Tree(mid + 1, right, nums);
            return newTreeNode;
        }
        public static void GetTreePreOrderByRecursion(TreeNode<T> root)
        {
            if (root == null)
            {
                return;
            }
            Console.Write($"{root.Data}\t");
            GetTreePreOrderByRecursion(root.LeftNode);
            GetTreePreOrderByRecursion(root.RightNode);
        }
        /// <summary>
        /// 分析前序的遍历方式，不难将递归转换为循环
        /// </summary>
        /// <param name="root"></param>
        public static void GetTreePreOrderNotRecursion(TreeNode<T> root)
        {
            if (root == null)
            {
                return;
            }
            LinkStack<TreeNode<T>> myStack = new LinkStack<TreeNode<T>>();
            while (!myStack.IsEmpty() || root != null)//外层整体循环
            {
                while (root != null)//内层每个节点的循环
                {
                    Console.Write($"{root.Data}\t");
                    myStack.Push(root);
                    root = root.LeftNode;
                }
                root = myStack.Pop().RightNode;
            }
        }
        public static void GetTreeInOrderByRecursion(TreeNode<T> root)
        {
            if (root == null)
            {
                return;
            }
            GetTreeInOrderByRecursion(root.RightNode);
            Console.Write($"{root.Data}\t");
            GetTreeInOrderByRecursion(root.LeftNode);
        }
        /// <summary>
        /// 与前序的输出顺序
        /// </summary>
        /// <param name="root"></param>
        public static void GetTreeInOrderNotRecursion(TreeNode<T> root)
        {
            if (root == null)
            {
                return;
            }
            LinkStack<TreeNode<T>> myStack = new LinkStack<TreeNode<T>>();
            while (!myStack.IsEmpty() || root != null)
            {
                while (root != null)
                {
                    myStack.Push(root);
                    root = root.RightNode;
                }
                root = myStack.Pop();
                Console.Write($"{root.Data}\t");
                root = root.LeftNode;
            }
        }
        public static void GetTreePostOrderByRecursion(TreeNode<T> root)
        {
            if (root == null)
            {
                return;
            }
            GetTreePostOrderByRecursion(root.LeftNode);
            GetTreePostOrderByRecursion(root.RightNode);
            Console.Write($"{root.Data}\t");
        }
        /// <summary>
        /// 比较复杂，需静心思考！
        /// 一整套流程下来，神清气爽的！
        /// </summary>
        /// <param name="root"></param>
        public static void GetTreePostOrderNotRecursion(TreeNode<T> root)
        {
            if (root == null)
            {
                return;
            }
            LinkStack<TreeNode<T>> myStack = new LinkStack<TreeNode<T>>();
            TreeNode<T> flagNode = null;//主要针对含有右节点的节点，一旦右节点访问完毕，根节点也可以访问了
            while (root != null)
            {
                myStack.Push(root);
                root = root.LeftNode;
            }
            while(!myStack.IsEmpty())
            {
                root = myStack.Pop();
                if (root.RightNode == null || root.RightNode == flagNode)
                {
                    Console.Write($"{root.Data}\t");
                    flagNode = root;//记录访问的右节点，下次根节点过来时，可以正常执行
                }
                else
                {
                    myStack.Push(root);
                    root = root.RightNode;
                    while (root != null)
                    {
                        myStack.Push(root);
                        root = root.LeftNode;
                    }
                }
            }

        }
        public static void GetTreeLevelOrder(TreeNode<T> root)
        {
            if (root == null)
            {
                return;
            }
            LinkQueue<TreeNode<T>> myQueue = new LinkQueue<TreeNode<T>>();
            TreeNode<T> tempNode = root;
            myQueue.In(tempNode);
            while (myQueue.Count > 0)
            {
                tempNode = myQueue.Out();
                Console.Write($"{tempNode.Data}\t");
                if (tempNode.LeftNode != null)
                {
                    myQueue.In(tempNode.LeftNode);
                }
                if (tempNode.RightNode != null)
                {
                    myQueue.In(tempNode.RightNode);
                }
            }
        }
        public static TreeNode<T> DFSCloneTree(TreeNode<T> root)
        {
            if (root == null)
            {
                return null;
            }
            TreeNode<T> newTreeNode = new TreeNode<T>(root.Data);
            newTreeNode.LeftNode = DFSCloneTree(root.LeftNode);
            newTreeNode.RightNode = DFSCloneTree(root.RightNode);
            return newTreeNode;
        }
        public static TreeNode<T> BFSCloneTree(TreeNode<T> root)
        {
            if (root == null)
            {
                return null;
            }
            LinkQueue<TreeNode<T>> myQueue = new LinkQueue<TreeNode<T>>();
            TreeNode<T> originTreeNode = root;
            TreeNode<T> newTreeNode = new TreeNode<T>(root.Data);
            TreeNode<T> tempTreeNode = null;
            myQueue.In(originTreeNode);
            myQueue.In(newTreeNode);
            while (myQueue.Count > 0)
            {
                originTreeNode = myQueue.Out();
                tempTreeNode = myQueue.Out();
                if (originTreeNode.LeftNode != null)
                {
                    tempTreeNode.LeftNode = new TreeNode<T>(originTreeNode.LeftNode.Data);
                    myQueue.In(originTreeNode.LeftNode);
                    myQueue.In(tempTreeNode.LeftNode);
                }
                if (originTreeNode.RightNode != null)
                {
                    tempTreeNode.RightNode = new TreeNode<T>(originTreeNode.RightNode.Data);
                    myQueue.In(originTreeNode.RightNode);
                    myQueue.In(tempTreeNode.RightNode);
                }
            }
            return newTreeNode;
        }
        public static void SwapTreeNode(ref TreeNode<T> p,ref TreeNode<T> q)
        {
            TreeNode<T> temp = p;
            p = q;
            q = temp;
        }
        public static void MirrorTree(TreeNode<T> root)
        {
            if (root == null)
            {
                return;
            }
            MirrorTree(root.LeftNode);
            SwapTreeNode(ref root._leftNode, ref root._rightNode);
            MirrorTree(root.RightNode);
        }
        public static bool IsCompleteBinaryTree(TreeNode<T> root)
        {
            if (root == null)
            {
                return false;
            }
            LinkQueue<TreeNode<T>> myQueue = new LinkQueue<TreeNode<T>>();
            TreeNode<T> tempNode = root;
            myQueue.In(tempNode);
            while (tempNode!= null)
            {
                tempNode = myQueue.Out();
                myQueue.In(tempNode.LeftNode);
                myQueue.In(tempNode.RightNode);
            }
            while (myQueue.Count > 0)
            {
                tempNode = myQueue.Out();
                if (tempNode != null)
                {
                    return false;
                }
            }
            return true;
        }
        public static int GetTreeHeightByRecursion(TreeNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            if (root.LeftNode == null && root.RightNode == null)
            {
                return 1;
            }
            int leftHeight = GetTreeHeightByRecursion(root.LeftNode);
            int rightHeight = GetTreeHeightByRecursion(root.RightNode);
            return leftHeight > rightHeight ? leftHeight : rightHeight + 1;
        }
        public static int CountLeafTreeNode(TreeNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            if (root.LeftNode == null && root.RightNode == null)
            {
                return 1;
            }
            return CountLeafTreeNode(root.LeftNode) + CountLeafTreeNode(root.RightNode);
        }
        /// <summary>
        /// 思路：高度即是树的层数
        /// 使用BFS层次遍历树，每经过一层，高度加一即可
        /// 计算叶子结点也可以通过BFS遍历，对每个节点进行判断
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int GetTreeHeightNotRecursion(TreeNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            LinkQueue<TreeNode<T>> myQueue = new LinkQueue<TreeNode<T>>();
            myQueue.In(root);
            TreeNode<T> tempNode = null;
            int treeHeight = 0;
            int queueLen = 0;//添加到队列中的个数，即是每一层的个数
            while (myQueue.Count > 0)
            {
                queueLen = myQueue.Count;
                tempNode = myQueue.Out();
                treeHeight++;
                while (queueLen-- > 0)//确保这一层的节点都访问完毕，退出该循环时，队列中的元素均是下层的节点
                {
                    if (tempNode.LeftNode != null)
                    {
                        myQueue.In(tempNode.LeftNode);
                    }
                    if (tempNode.RightNode != null)
                    {
                        myQueue.In(tempNode.RightNode);
                    }
                    if (!myQueue.IsEmpty())
                    {
                        tempNode = myQueue.Out();
                    }
                }
            }
            return treeHeight;
         }
        public static int GetTreeNodeNum(TreeNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            return GetTreeNodeNum(root.LeftNode) + GetTreeNodeNum(root.RightNode) + 1;
        }

    }
}
