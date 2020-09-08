using System;
using System.Collections.Generic;
using System.Text;

namespace 数据结构
{
    public class Sequencelist<T> : ILinearList<T>
    {

        public Sequencelist(int length=10)
        {
            if (length > MaxLength)
            {
                length = MaxLength;
            }
            if (length <= 0)
            {
                length = 10;
            }
            Datas = new T[length];
        }

        private  T[] Datas =null;
        public int MaxLength { get; } = 100;

        private int _Index = -1;

        public int Size => _Index;

        public int Length
        {
            get
            {
                if (Datas == null)
                    return -1;
                else
                {
                    return Datas.Length;
                }
            }
        }

        public bool IsFull()
        {
            if (_Index +1>= Length)
            {
                return true;
            }
            else {

                return false;
            }
        }

        public void Add(T item)
        {
            if (IsFull())
            {
                Console.WriteLine("顺序表空间已满，不能加入！");
                return;
            }
            else
            {
                _Index++;
                Datas[_Index] = item;
            }
        }

        public void Clear()
        {
            for (int i = 0; i <=_Index; i++)
            {
                Datas[i] = default(T);
            }
            _Index = -1;
        }

        public T GetItem(int index)
        {
            if (Datas == null || index > _Index)
                return default(T);
            else
            {
                return Datas[index];
            }
        }

        public void Insert(T item, int index)
        {
            if (IsFull())
            {
                throw new Exception("线性表已满！操作失败！");
            }
            else {
                for (int i = _Index; i >=index; i--)
                {
                    Datas[i + 1] = Datas[i];
                }
                Datas[index] = item;
                _Index++;
            }
        }

        public bool IsEmpty()
        {
            return _Index <= 0;
        }

        public int OfIndex(T item)
        {
            if (IsEmpty())
            {
                return -1;
            }
            else {
                for (int i = 0; i <= _Index; i++)
                {
                    if (Datas[i].Equals(item))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public T Remove(T item)
        {
            if (IsEmpty())
            {
                return default(T) ;
            }
            else
            {
                int index = OfIndex(item);
                return Remove(index);
            }
        }

        public T Remove(int index)
        {
            if (index >= 0 && index <= _Index)
            {
                T item = GetItem(index);
                for (int i = index; i < _Index; i++)
                {
                    Datas[i] = Datas[i + 1];
                }
                _Index--;
                return item;
            }
            else
            {
                return default(T);
            }
        }

        public void SetItem(int index, T item)
        {
            if (index <= _Index || index >= 0)
            {
                Datas[index] = item;
            }
            else {
                throw new Exception("索引超出满范围！");
            }
        }


        public void PrinterForItme()
        {
            for (int i = 0; i < _Index; i++)
            {
                Console.WriteLine(Datas[i].ToString());
            }
            Console.ReadKey();
        }
    }
}
