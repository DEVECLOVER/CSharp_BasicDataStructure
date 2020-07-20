using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeAndGraph
{
    interface ITree<T>
    {
        Node<T> Root();
        Node<T> GetLeftChild(Node<T> node);
        Node<T> GetRightChild(Node<T> node);
        void InsertLeftChild(T data,Node<T> node);
        void InsertRightChild(T data,Node<T> node);
        Node<T> DeleteLeftChild(Node<T> node);
        Node<T> DeleteRightChild(Node<T> node);
        bool IsLeafNode(Node<T> node);
        bool IsEmpty();
    }
}
