using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;

namespace LearningDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ////Application.EnableVisualStyles();
            ////Application.SetCompatibleTextRenderingDefault(false);
            ////Application.Run(new Form1());
            //clsStack<int> stack = new clsStack<int>(5);
            //stack.Push(2);
            //stack.Push(91);
            //stack.Push(3);
            //stack.Push(10);
            //stack.Push(6);

            //Console.WriteLine(stack.Pop());
            //Console.WriteLine(stack.Pop());
            //Console.WriteLine(stack.Pop());

            //Console.WriteLine(stack.Pop());
            //Console.WriteLine(stack.Pop());
            //Console.Read();



            //Hashtable ht = new Hashtable();
            //ht.Add(1,"adsa");
            //ht.Add(2, "adsa");
            //ht.Add(3, "adsa");
            //ht.Add(4, "adsa");
            //ht.Add(5, "adsa");

            //foreach (DictionaryEntry item in ht)
            //{
            //    Console.WriteLine(item.Key + "|" + item.Value);
            //}


           // mybitArray();



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void mybitArray()
        {
            BitArray myBA1 = new BitArray(5);

            BitArray myBA2 = new BitArray(5, false);

            byte[] myBytes = new byte[5] { 1, 2, 3, 4, 5 };
            BitArray myBA3 = new BitArray(myBytes);

            bool[] myBools = new bool[5] { true, false, true, true, false };
            BitArray myBA4 = new BitArray(myBools);

            int[] myInts = new int[5] { 6, 7, 8, 9, 10 };
            BitArray myBA5 = new BitArray(myInts);

            // Displays the properties and values of the BitArrays.
            Console.WriteLine("myBA1");
            Console.WriteLine("   Count:    {0}", myBA1.Count);
            Console.WriteLine("   Length:   {0}", myBA1.Length);
            Console.WriteLine("   Values:");
            PrintValues(myBA1, 8);

            Console.WriteLine("myBA2");
            Console.WriteLine("   Count:    {0}", myBA2.Count);
            Console.WriteLine("   Length:   {0}", myBA2.Length);
            Console.WriteLine("   Values:");
            PrintValues(myBA2, 8);

            Console.WriteLine("myBA3");
            Console.WriteLine("   Count:    {0}", myBA3.Count);
            Console.WriteLine("   Length:   {0}", myBA3.Length);
            Console.WriteLine("   Values:");
            PrintValues(myBA3, 8);

            Console.WriteLine("myBA4");
            Console.WriteLine("   Count:    {0}", myBA4.Count);
            Console.WriteLine("   Length:   {0}", myBA4.Length);
            Console.WriteLine("   Values:");
            PrintValues(myBA4, 8);

            Console.WriteLine("myBA5");
            Console.WriteLine("   Count:    {0}", myBA5.Count);
            Console.WriteLine("   Length:   {0}", myBA5.Length);
            Console.WriteLine("   Values:");
            PrintValues(myBA5, 8);
        }

        public static void PrintValues(IEnumerable myList, int myWidth)
        {
            int i = myWidth;
            foreach (Object obj in myList)
            {
                Console.Write( obj);
            }
            Console.WriteLine();
        }
    }
}
