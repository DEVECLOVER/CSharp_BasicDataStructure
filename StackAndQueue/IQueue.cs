using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAndQueue
{
    public interface IQueue<T>
    {
        int GetLength(); //求队列的长度
        bool IsEmpty(); //判断对列是否为空
        void Clear(); //清空队列
        void In(T item); //入队
        T Out(); //出队
        T GetFront(); //取对头元素
    }
}
