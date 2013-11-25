/*
 * Current implementation of unrolled loop yeilds 50% speedup
 * over standard loop (single accumulator) on 2.0 GHz i7 Quad-Core, 4GB main mem.
 * Uses only integer pipelines.
 */

using System;
using System.Diagnostics;

namespace LoopUnrolling
{
    class Program
    {
        static void Main()
        {
            int n = 10000000;
            int[] data = new int[n];
            int sum, sum2, sum3, sum4;

            for (int i = 0; i < n; i++)
                data[i] = ++i;
            
            Stopwatch sw = Stopwatch.StartNew();
            for (int j = 0; j < 1000; j++)
            {
                sum = 0;
                sum2 = 0;
                sum3 = 0;
                sum4 = 0;
                for (int i = 0; i < n; i += 4)
                {
                    sum += data[i];
                    sum2 += data[i + 1];
                    sum3 += data[i + 2];
                    sum4 += data[i + 3];
                }

                sum = sum + sum2 + sum3 + sum4;                
            }
            sw.Stop();
            sw.Reset();

            double d = sw.ElapsedMilliseconds / 1000.0;
            Console.WriteLine("Unrolled loop with 4 accumulators - Time elapsed: " + d);


            int[] data2 = new int[n];

            for (int i = 0; i < n; i++)
                data2[i] = ++i;
            
            sw.Start();
            for (int j = 0; j < 1000; j++)
            {
                sum = 0;
                for (int i = 0; i < n; i ++)                
                    sum += data2[i];
            }
            sw.Stop();

            double d2 = sw.ElapsedMilliseconds / 1000.0;
            Console.WriteLine("\nSingle accumulator - Time elapsed: " + d2);
        }//Main
    }//class
}//namespace
