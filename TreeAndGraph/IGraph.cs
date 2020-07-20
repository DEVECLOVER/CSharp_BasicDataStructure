using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeAndGraph
{
    interface IGraph<T>
    {
        //获取顶点数
        int GetNumOfVertex();
        //获取边或弧的数目
        int GetNumOfEdge();
        //在两个顶点之间添加权为v的边或弧
        void SetEdge(GraphNode<T> v1, GraphNode<T> v2, int v);
        //删除两个顶点之间的边或弧
        void DelEdge(GraphNode<T> v1, GraphNode<T> v2);
        //判断两个顶点之间是否有边或弧
        bool IsEdge(GraphNode<T> v1, GraphNode<T> v2);
        bool IsNode(GraphNode<T> v);
        int GetIndex(GraphNode<T> v);
        int GetMatrix(int i, int j);
        GraphNode<T> GetNode(int index);
        void SetNode(GraphNode<T> v, int index);
    }
}
