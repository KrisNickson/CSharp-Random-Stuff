using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class Program
{

    /// <summary>
    /// Searches for an element in a SORTED array and returns its index if it finds it
    /// </summary>
    /// <param name="arr">The array to search</param>
    /// <param name="lowBound">Starting point</param>
    /// <param name="highBound">End point</param>
    /// <param name="value">Value to search for</param>
    /// <returns>Index indicating where the value we search for is located. If the value is not found returns -1.</returns>
    public static int BinarySearch(int[] arr, int lowBound, int highBound, int value)
    {
        int mid;
        while (lowBound <= highBound)
        {
            mid = (lowBound + highBound) / 2;
            if (arr[mid]<value)//the element we search is located to the right from the mid point
            {
                lowBound = mid + 1;
                continue;
            }
            else if (arr[mid] > value)//the element we search is located to the left from the mid point
            {
                highBound = mid - 1;
                continue;
            }
            //at this point low and high bound are equal and we have found the element or
            //arr[mid] is just equal to the value => we have found the searched element
            else 
            {
                return mid;
            }
        }
        return -1;//value not found
    }


    //Lets assume we have a number (N) of pipes varied in length. We want to slice them in M pieces (M>N) while maintaning 
    //maximum possible length of those M pieces.
    //Each of those M pieces have the exact same length. Baically we need to find the maxmim number able to devide 
    //a set of numbers in such way that the sum of the divisions will be M

    //Example: W have 3 pipes with lengths 100,200 and 300. We want to get from them 6 pipes with maximal length.
    //So we are searching for this maxmil length. In this case this length is 100

    //We search for a value in a certain range (not an array) for example: [1 - 100]
    //In this modification we dont know the exact value we are seaching for. Instead we know some conditions that tha value
    //must meet. The extent to which the current value (mid) meets those conditions dictates where the algorthim will 
    //search next relative the it (left or right from mid)
    //This will return the first value which meets the condition
    public static int BinarySearch2(int[] arr, int lowBound, int highBound, int condition)
    {
        int mid;
        while (lowBound <= highBound)
        {
            mid = (lowBound + highBound) / 2;
            int evaluator = Evaluate(arr, mid);

            //if we have managed to slice by that size, less pipes than we need
            //then the size is bigger than the actual max size, So we must decrease
            //the length(mid) by which we devide (slice)
            if (evaluator < condition)
            {
                highBound = mid - 1;
                continue;
            }
            else if (evaluator > condition)//
            {
                lowBound = mid + 1;
                continue;
            }
            else 
            {
                return mid;
            }
        }
        return -1;//value not found
    }

    private static int Evaluate(int[] arr, int mid)
    {
        int count = 0;
        int temp = 0;
        foreach (int pipe in arr)
        {
            temp = pipe / mid;
            count = count + temp;
            if (temp == 0)
            {
                break;
            }
        }

        return count;
    }

    
    static void Main()
    {
        int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        int val = 122;
        int index = BinarySearch(arr,0,arr.Length-1, val);
        int[] arr2 = new int[] { 444, 555, 777, 803 };
        int val2 = 11;
        int len = BinarySearch2(arr2, 1, 234, 11);

    }
}

