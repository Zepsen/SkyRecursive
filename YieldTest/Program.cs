using System;
using System.Collections.Generic;

namespace YieldTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            M1(0, 0);

            for (int i = 0; i < 4; i++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Console.Write($" {arr[i, y]}");
                }

                Console.WriteLine();
            }

            Console.WriteLine(a);
        }

        static int[,] arr = GetMockArr();
        static int a = 0;
        static bool finish = false;

        static void M1(int x, int y, int prev = 0)
        {
            a++;

            if (finish) return;

            // skip that already set
            // this is not needed
            while(AlreadySet(x, y) && !finish)
            {
                Next(ref x, ref y);
            }

            foreach (var item in GetNum(prev))
            {
                if (Check(x, y, item))
                {
                    arr[x, y] = item;
                    Next(ref x, ref y);
                    M1(x, y);
                }
            }

            if (finish) return;
            
            Back(ref x, ref y);
            var val = arr[x, y];
            arr[x, y] = 0;
            M1(x, y, val);
        }

        private static bool AlreadySet(int x, int y)
        {
            return arr[x, y] > 0;
        }

        private static bool Check(int x, int y, int item)
        {
            for (int i = 0; i < 4; i++)
            {
                //row
                if (arr[x, i] == item) return false;

                //col
                if (arr[i, y] == item) return false;
            }

            // 3 add constrains check 

            return true;
        }

        private static void Back(ref int x, ref int y)
        {
            if (x == 0 && y == 0)
            {
                finish = true;
                return;
            }

            if (y == 0) y = 3; else y -= 1;
            if (y == 3) x -= 1;
        }

        static void Next(ref int x, ref int y)
        {
            if (x == 3 && y == 3)
            {
                finish = true;
                return;
            }

            if (y == 3) y = 0; else y += 1;
            if (y == 0) x += 1; 
        }
       
        static IEnumerable<int> GetNum(int n)
        {
            for (int i = n + 1; i < 5; i++)
            {
                yield return i;
            }
        }

        static int[,] GetMockArr()
        {
            return new int[4, 4]
            {
                { 0, 0, 0, 0,},
                { 4, 0, 0, 2,},
                { 0, 0, 0, 3,},
                { 0, 0, 0, 4,}
            };
        }
    }
}
