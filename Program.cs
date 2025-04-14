using System;
using System.Collections.Generic;

class Program
{
    static HashSet<int> disallowedValues_n = new HashSet<int> { 7, 8, 9 };
    static HashSet<int> disallowedValues_t = new HashSet<int> { 0, 2, 3, 4, 7, 9 };
    static HashSet<int> disallowedValues_h = new HashSet<int> { 1, 2, 3, 5, 6, 7, 9 };
    static HashSet<int> disallowedValues_e = new HashSet<int> { 0, 1, 2 };
    static HashSet<int> disallowedValues_a = new HashSet<int> { 5 };
    static HashSet<int> disallowedValues_s = new HashSet<int> { 3, 6, 7, 8, 9 };
    static HashSet<int> disallowedValues_u = new HashSet<int> { 0, 5, 8 };
    static HashSet<int> disallowedValues_w = new HashSet<int> { 5, 9 };

    static List<string> permutationsList = new List<string>();

    static void Main()
    {
        int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        var startTime = DateTime.Now;
        Permute(numbers, 0, numbers.Length - 1);
        var timeTaken = DateTime.Now - startTime;
        Console.WriteLine($"Time taken: {timeTaken}");


        // Print all permutations at the end
        var count = 0;
        foreach (var perm in permutationsList)
        {
            count++;
            Console.WriteLine($"{count}: {perm}");
        }
    }

    static void Permute(int[] arr, int start, int end)
    {
        if (start == end)
        {
            //permutations++;

            // Check conditions using pre-initialized HashSets
            bool case5 = !disallowedValues_h.Contains(arr[4]);
            if (!case5) return;

            bool case4 = !disallowedValues_t.Contains(arr[3]);
            if (!case4) return;

            bool case8 = !disallowedValues_s.Contains(arr[7]);
            if (!case8) return;

            bool case1 = !disallowedValues_n.Contains(arr[0]);

            if (!case1) return;

            bool case6 = !disallowedValues_e.Contains(arr[5]);
            if (!case6) return;

            bool case9 = !disallowedValues_u.Contains(arr[8]);
            if (!case9) return;

            bool case10 = !disallowedValues_w.Contains(arr[9]);
            if (!case10) return;

            bool case7 = !disallowedValues_a.Contains(arr[6]);
            if (!case7) return;

            bool caseY = (arr[9] != 5 && arr[9] != 9);
            if (!caseY) return;

            bool caseZ = (arr[0] != 7 && arr[0] != 8 && arr[0] != 9);
            if (!caseZ) return;

            bool caseA = (arr[3] + arr[3] + arr[4] + arr[4]) % 10 == arr[4];
            if (!caseA) return;

            bool caseC = arr[0] + arr[7] < 10;
            if (!caseC) return;

            int north = arr[0] * 10000 + arr[1] * 1000 + arr[2] * 100 + arr[3] * 10 + arr[4];
            int east = arr[5] * 1000 + arr[6] * 100 + arr[7] * 10 + arr[3];
            int south = arr[7] * 10000 + arr[1] * 1000 + arr[8] * 100 + arr[3] * 10 + arr[4];
            int west = arr[9] * 1000 + arr[5] * 100 + arr[7] * 10 + arr[3];
            int earth = arr[5] * 10000 + arr[6] * 1000 + arr[2] * 100 + arr[3] * 10 + arr[4];

            if (north + south + east + west == earth)
            {
                permutationsList.Add($"{arr[0]},{arr[1]},{arr[2]},{arr[3]},{arr[4]},{arr[5]},{arr[6]},{arr[7]},{arr[8]},{arr[9]}");
            }
        }
        else
        {
            for (int i = start; i <= end; i++)
            {
                Swap(ref arr[start], ref arr[i]);
                Permute(arr, start + 1, end);
                Swap(ref arr[start], ref arr[i]); // backtrack
            }
        }
    }

    static void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }
}