using System;

namespace 数据结构
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            Sequencelist<string> lt = new Sequencelist<string>(10);
            lt.Add("第1次");
            lt.Add("第2次");
            lt.Add("第3次");
            lt.Add("第4次");
            lt.Remove("第2次");
            lt.Add("第6次");
            lt.Insert("第7次",2);
            lt.OfIndex("第7次");
            lt.Add("第9次");
            lt.Add("第10次");
            lt.Add("第11次");
            lt.Add("第一次");
            lt.PrinterForItme();
        }
    }
}
