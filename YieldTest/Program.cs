using System;
using System.Collections.Generic;
using System.Linq;

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
        static int[] cnstrs = new[] {
            0, 0, 0, 0,   // >
            0, 0, 0, 3,   // V
            0, 0, 0, 0,   // <
            0, 0, 0, 0 }; // ^

        static int _size = 4;

        static void M1(int x, int y, int prev = 0)
        {
            a++;

            if (finish) return;

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

        /// <summary>
        /// This is needed if arr already contains nums
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool AlreadySet(int x, int y)
        {
            return arr[x, y] > 0;
        }

        private static bool Check(int x, int y, int item)
        {
            if (!IfItemAlreadyInRowOrInCol(x, y, item)) return false;

            if (!CheckConditions(x, y, item)) return false;

            return true;
        }

        private static bool CheckConditions(int x, int y, int item)
        {
            var res = true;
            var ctrs = GetConstrains(x, y);

            if (ctrs.All(i => i == 0)) return true;

            arr[x, y] = item;

            if (ctrs.Any(i => i == 1))
                res = Cons1(ctrs, x, y);

            if (!res)
            {
                arr[x, y] = 0;
                return false;
            }


            if (ctrs.Any(i => i == 4))
                res = Cons4(ctrs, x, y);

            if (!res)
            {
                arr[x, y] = 0;
                return res;
            }

            if (ctrs.Any(i => i == 3))
                res = Cons3(ctrs, x, y);

            if (!res)
            {
                arr[x, y] = 0;
                return res;
            }

            return res;
        }

        private static bool Cons3(List<int> ctrs, int x, int y)
        {
            if (ctrs[0] == 3)
            {
                if (arr[x, 0] == 4 ||
                    arr[x, 1] == 4 ||
                    arr[x, 0] == 3)
                    return false;
                
                for (int i = 0; i < _size; i++)
                {
                    if (arr[x, i] == 0) return true;
                }

                var see = 1;
                var min = arr[x, 0];
                for (int i = 0; i < _size; i++)
                {
                    if (arr[x, i] > min) see++;
                }

                return see == 3;
            }

            if (ctrs[1] == 3)
            {
                if (arr[x, 3] == 4 ||
                    arr[x, 2] == 4 ||
                    arr[x, 3] == 3)
                    return false;

                for (int i = 0; i < _size; i++)
                {
                    if (arr[x, _size - 1 - i] == 0) return true;
                }

                var see = 1;
                var min = arr[x, 3];
                for (int i = 0; i < _size; i++)
                {
                    if (arr[x, _size - 1 - i] > min) see++;
                }

                return see == 3;
            }

            if (ctrs[2] == 3)
            {
                if (arr[0, y] == 4 ||
                    arr[1, y] == 4 ||
                    arr[0, y] == 3)
                    return false;
                for (int i = 0; i < _size; i++)
                {
                    if (arr[i, y] == 0) return true;
                }

                var see = 1;
                var min = arr[0, y];
                for (int i = 0; i < _size; i++)
                {
                    if (arr[i, y] > min) see++;
                }

                return see == 3;
            }

            if (ctrs[3] == 3)
            {
                if (arr[3, y] == 4 ||
                    arr[2, y] == 4 ||
                    arr[3, y] == 3)
                    return false;

                for (int i = 0; i < _size; i++)
                {
                    if (arr[_size - 1 - i, y] == 0) return true;
                }

                var see = 1;
                var min = arr[3, y];
                for (int i = 0; i < _size; i++)
                {
                    if (arr[_size - 1 - i, y] > min) see++;
                }

                return see == 3;
            }

            return true;
        }

        /// <summary>
        /// Check if constrain 4
        /// </summary>
        /// <param name="ctrs"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool Cons4(List<int> ctrs, int x, int y)
        {
            if (ctrs[0] == 4)
            {
                for (int i = 0; i < _size; i++)
                {
                    if (arr[x, i] == i + 1 || arr[x, i] == 0)
                        continue;

                    return false;
                }
            }

            if (ctrs[1] == 4)
            {
                for (int i = 0; i < _size; i++)
                {
                    if (arr[x, _size - 1 - i] == i + 1 || arr[x, _size - 1 - i] == 0)
                        continue;

                    return false;
                }
            }

            if (ctrs[2] == 4)
            {
                for (int i = 0; i < _size; i++)
                {
                    if (arr[i, y] == i + 1 || arr[i, y] == 0)
                        continue;

                    return false;
                }
            }

            if (ctrs[3] == 4)
            {
                for (int i = 0; i < _size; i++)
                {
                    if (arr[_size - 1 - i, y] == i + 1 || arr[_size - 1 - i, y] == 0)
                        continue;

                    return false;
                }
            }

            return true;
        }

        private static List<int> GetConstrains(int x, int y)
        {
            var lRow = cnstrs[_size * _size - 1 - x];
            var rRow = cnstrs[_size * 2 - 1 - x];
            var lCol = cnstrs[y];
            var rCol = cnstrs[_size * 3 - 1 - y];

            return new List<int> { lRow, rRow, lCol, rCol };
        }

        /// <summary>
        /// Check if constrains 1
        /// </summary>
        /// <param name="ctrs"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool Cons1(List<int> ctrs, int x, int y)
        {
            if (ctrs[0] == 1)
            {
                return arr[x, 0] == 4 || arr[x, 0] == 0;
            }

            if (ctrs[1] == 1)
            {
                return arr[x, 3] == 4 || arr[x, 3] == 0;
            }

            if (ctrs[2] == 1)
            {
                return arr[0, y] == 4 || arr[0, y] == 0;
            }

            if (ctrs[3] == 1)
            {
                return arr[3, y] == 4 || arr[3, y] == 0;
            }

            return true;
        }

        private static bool IfItemAlreadyInRowOrInCol(int x, int y, int item)
        {
            for (int i = 0; i < _size; i++)
            {
                //row
                if (arr[x, i] == item) return false;

                //col
                if (arr[i, y] == item) return false;
            }

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
                { 0, 0, 0, 0,},
                { 0, 0, 0, 0,},
                { 0, 0, 0, 0,}
            };
        }
    }
}
