using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace 数据结构
{
    public interface ILinearList<T> 
    {
        int Length { get; }
        bool IsEmpty();
        void Clear();
        T GetItem(int index);
        void SetItem(int index, T item);
        void Add(T item);
        void Insert(T item, int index);
        T Remove(T item);
        T Remove(int index);
        int OfIndex(T item);
    }
}
