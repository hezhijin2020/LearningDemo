using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningDemo.cls
{

    /// <summary>
    /// 通用的泛型类栈
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class clsStack<T>
    {
        private T[] stack;
        public int stackPoint { get; }
        public int size { get; }

        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <param name="size"></param>
        public clsStack(int size)
        {
            this.size = size;
            this.stack = new T[size];
            this.stackPoint = -1;
        }

        /// <summary>
        /// 入栈方法
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            if (stackPoint >= size)
            {
                throw new Exception("栈空间已满！");
            }
            else
            {
                stackPoint++;
                this.stack[stackPoint] = item;
            }
        }

        /// <summary>
        /// 出栈方法
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            T data = this.stack[stackPoint];
            stackPoint--;
            return data;
        }
    }
}
