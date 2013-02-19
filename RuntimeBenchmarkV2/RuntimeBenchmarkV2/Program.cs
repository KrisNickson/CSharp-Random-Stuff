using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

//Written by Kristian Nikolov 2012
//http://zeroreversesoft.wordpress.com/

class Benchmark
{

    //Place methods here (if there are any)!
    public static int CharToInt1(char input)
    {
        int result = -1;

        if (input >= 48 && input <= 57)
        {
            result = input - '0';
        }

        return result;
    }


    public static int CharToInt2(char input)
    {
        int result = -1;

        int tempInt = 0;
        if (int.TryParse(input.ToString(), out tempInt) == true)
        {
            result = tempInt;
        }


        return result;
    }

    public static int CharToInt3(char input)
    {
        double temp = Char.GetNumericValue(input);
        int result = Convert.ToInt32(temp);
        return result;
    }


    static void Main()
    {
        Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2); //always use the second cpu/cpu core
        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;//set high process priority
        Thread.CurrentThread.Priority = ThreadPriority.Highest;//set high thread priority
        Stopwatch sw = new Stopwatch();
        List<decimal> times = new List<decimal>();
        decimal avarage = 0m;
        int repetitions = 200; //warmup repetitions


        //define input here!
        char[] input = { '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',
                        '1', '2', 'e', '8', '9', '0', 'r', 's', 'z', '@', '7', '6', '2',}; //208 elements


        //first execution is warm up, second execution is the benchmark
        bool initializeBenchmark = false;
        for (int ex = 0; ex <= 1; ex++)
        {
            if (ex == 0)
            {
                initializeBenchmark = true;
            }

            for (int i = 1; i <= repetitions; i++)
            {
                sw.Start();

                //stuff to be benchmarked start

                foreach (char c in input)
                {
                    CharToInt3(c);
                }

                //stuff to be benchmarked end

                sw.Stop();

                times.Add(Convert.ToDecimal(sw.Elapsed.TotalSeconds));
                avarage = avarage + times[i - 1];
                sw.Reset();
            }

            //reset the list and set the repetitions for the real test
            if (initializeBenchmark == true)
            {
                times.RemoveRange(0, times.Count);
                repetitions = 400;//actual repetitions for benchmarking
                initializeBenchmark = false;
            }
        }

        avarage = avarage / times.Count;

        times.Sort();
        Console.WriteLine("\n\n\nMinimum runtime: {0} seconds", times[0]);
        Console.WriteLine("\nMaximum runtime: {0} seconds", times[times.Count - 1]);
        Console.WriteLine("\nAvarage runtime: {0} seconds\n", avarage);


    }
}


