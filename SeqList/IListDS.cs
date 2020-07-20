namespace SeqList
{
    interface IListDS<T>
    {
        void AddElement(T item);
        void RemoveElement(T index);
        void Insert(T item, int index);
        void ChangeElement(T item,int index);
        void Clear();
        bool IsEmpty();
        T this[int index] { get; set; }
        T GetElement(int index);
        int Locate(T item);
        int GetLength();
    }
}
