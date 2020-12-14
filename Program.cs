using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Test(new int[2][]
            {
                new int[3] {1, 0, 1},
                new int[3] {0, 1, 0}
            }, 3, "1");

            Test(new int[3][]
            {
                new int[2] {1, 0},
                new int[2] {1, 0},
                new int[2] {1, 1}
            }, 1, "2");

            Test(new int[3][]
            {
                new int[2] {1, 0},
                new int[2] {1, 0},
                new int[2] {0, 1}
            }, 2, "3");

            Test(new int[2][]
            {
                new int[2] {0, 0},
                new int[2] {0, 0}
            }, 0, "4");

            Test(new int[1][]
            {
                new int[1] {0}
            }, 0, "5");
            
            Test(new int[1][]
            {
                new int[1] {1}
            }, 1, "6");
            
            Console.WriteLine("Success");
        }

        private static void Test(int[][] a, int checkCnt, string testName = "")
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Debug.Assert(CountIslands(a) == checkCnt);
            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            Console.WriteLine($"Test {testName} is successful. RunTime: {ts.Seconds}.{ts.Milliseconds / 10} seconds");
        }
        
        private static int CountIslands(int[][] cells)
        {
            var n = cells.Length;
            var m = cells[0].Length;
            var queueToProcess = new Queue<int>();
            var islandCnt = 0;
            //some kind of bfs
            for (var i = 0; i < n; i++)
                for (var j = 0; j < m; ++j)
                {
                    if (cells[i][j] == 1)
                    {
                        queueToProcess.Enqueue(i * m + j);
                        while (queueToProcess.Count > 0)
                        {
                            var jCoord = queueToProcess.Peek() % m;
                            var iCoord = queueToProcess.Peek() / m;
                            if (iCoord > 0 && cells[iCoord - 1][jCoord] == 1) //up
                                queueToProcess.Enqueue((iCoord - 1) * m + jCoord); 
                            if (jCoord > 0 && cells[iCoord][jCoord - 1] == 1) //left
                                queueToProcess.Enqueue(iCoord * m + (jCoord - 1));
                            if (iCoord < n - 1 && cells[iCoord + 1][jCoord] == 1) //down
                                queueToProcess.Enqueue((iCoord + 1) * m + jCoord); 
                            if (jCoord < m - 1 && cells[iCoord][jCoord + 1] == 1) //right
                                queueToProcess.Enqueue(iCoord * m + (jCoord + 1));
                            queueToProcess.Dequeue();
                            cells[iCoord][jCoord] = 2; //visited

                        }
                        islandCnt++;
                    }
                }
            
            
            return islandCnt;
        }

    }
}