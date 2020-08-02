using System;
namespace Tree
{
    class TreeNode<T> 
    {
        private T _data;
        public TreeNode<T> _leftNode;
        public TreeNode<T> _rightNode;
        public T Data { get { return _data; } set { _data = value; } }
        public TreeNode<T> LeftNode { get { return _leftNode; } set { _leftNode = value; } }
        public TreeNode<T> RightNode { get { return _rightNode; } set { _rightNode = value; } }
        public TreeNode()
        {
            _data = default(T);
            _leftNode = null;
            _rightNode = null;
        }
        public TreeNode(T data)
        {
            _data = data;
            _leftNode = null;
            _rightNode = null;
        }
    }
}
