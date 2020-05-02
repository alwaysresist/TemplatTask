using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLtree<int> tree = new AVLtree<int>();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);
            tree.Remove(4);
            bool search1 = tree.Find(1);
            bool search2 = tree.Find(4);

            Console.Write("Существует элемент 1:" + search1.ToString());
            Console.WriteLine();
            Console.Write("Существует элемент 4: " + search2.ToString());
            Console.WriteLine();

            int[] arr = tree.Leaves();
            for (int i = 0; i < tree.getCount(); i++)
                Console.Write("{0} ", arr[i]);
            Console.WriteLine();
        }
    }
}
