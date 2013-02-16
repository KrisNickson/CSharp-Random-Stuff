using System;
using System.Collections.Generic;



class Program
{
    //this method checks if there is 1 or 0 at the passed index
    //in the binary representation of the passed input
    public static byte CheckBitValueII(int input, int index)
    {
        int checker = 1 << index;

        int result = input & checker;

        if (result == checker)
        {
            return 1;
        }
        else
        {
            return 0;
        }

    }

    static void Main()
    {
        byte n = byte.Parse(Console.ReadLine()); //size of the set

        //input the set
        List<int> itemset = new List<int>();
        for (byte i = 0; i < n; i++)
        {
            itemset.Add(int.Parse(Console.ReadLine()));
        }

        //list of lists containing int values
        //this list will contain all the subsets
        //each subset is a list of integers
        List<List<int>> allSubsets = new List<List<int>>();

        int subsetCount = (int)Math.Pow(2, itemset.Count); //the count of the possible subsets

        //we take the number of each of the possible subsets in order to generate them
        //0 is for the empty subset
        for (int i = 0; i < subsetCount; i++)
        {
            List<int> subset = new List<int>();//a list to contain the generated subset

            //we use binary number to generate subsets
            //since the largest subset is as large as the original input set
            //the size of the binary number which we will use will be the
            //same as the size of the input set
            //the binary number is actually the index of the current
            //subset we want to generate
            //we check where in that binary number there are 1
            //and we take the items of the input set which are in the same position
            //(aka index) as the 1 in the binary number
            for (int bitIndex = 0; bitIndex < itemset.Count; bitIndex++)
            {
                if (CheckBitValueII(i, bitIndex) == 1)
                {
                    subset.Add(itemset[bitIndex]);
                }
            }

            allSubsets.Add(subset);//the subset is already generated and is now stored
            //then we go to the number of the next subset and the process repeats itself
        }

        



    }

    

}

