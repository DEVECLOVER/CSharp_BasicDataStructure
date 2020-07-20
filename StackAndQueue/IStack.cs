namespace StackAndQueue
{
    interface IStack<T>
    {
        int GetLength(); //求栈的长度
        bool IsEmpty(); //判断栈是否为空
        void Clear(); //清空操作
        void Push(T item); //入栈操作
        T Pop(); //出栈操作
        T GetTop(); //取栈顶元素
    }
}
