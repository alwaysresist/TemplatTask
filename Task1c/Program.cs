using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> que1 = new Queue<int>(3);
            Queue<int> que2 = new Queue<int>(4);
            que1.Push(1);
            que1.Push(2);
            int k = que1.Pop();
            que1.Push(1);
            que1.Push(2);
            que2.Push(3);
            que2.Push(4);
            que2.Push(5);
            que2.Push(6);

            Queue<int> que3 = (Queue<int>)que2.Clone();
            Console.WriteLine(que3.Pop());

            Queue<int> queCombined = Queue<int>.Combine(que1, que3);
            for(int i = 0; i < 6; i++)
                Console.Write("{0} ", queCombined.Pop());
            Console.ReadKey();
        }
    }
}
