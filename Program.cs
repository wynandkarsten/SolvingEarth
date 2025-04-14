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
        Console.WriteLine("Permutations:");
        Console.WriteLine("    N,O,R,T,H,E,A,S,U,W");
        foreach (var perm in permutationsList)
        {
            count++;
            Console.WriteLine($"{count,2}: {perm}");
        }
    }

    static bool IsValidCombination(int[] arr)
    {
        // Short-circuit: Hard fail conditions first (most likely to fail)
        if (arr[0] == 7 || arr[0] == 8 || arr[0] == 9) return false; // NORTH: N should not be 7, 8, 9

        // This was working, but currently does not. Go through the logic again and see what is wrong.
        
        // Before: (Used to work)
        //    bool caseY = (arr[9] != 5 && arr[9] != 9);
        //    if (!caseY) return;

        // After: (Broken)
        //if (arr[9] != 5 && arr[9] != 9) return false;                // WEST: W must be 5 or 9


        if (arr[0] + arr[7] >= 10) return false;                     // N + S < 10
        if ((arr[3] + arr[3] + arr[4] + arr[4]) % 10 != arr[4]) return false; // 2T + 2H % 10 == H

        // Word-based constraints
        // NORTH = N O R T H → indexes 0 1 2 3 4
        if (disallowedValues_n.Contains(arr[0])) return false; // N
        if (disallowedValues_t.Contains(arr[3])) return false; // T
        if (disallowedValues_h.Contains(arr[4])) return false; // H

        // EAST = E A S T → indexes 5 6 7 3
        if (disallowedValues_e.Contains(arr[5])) return false; // E
        if (disallowedValues_a.Contains(arr[6])) return false; // A
        if (disallowedValues_s.Contains(arr[7])) return false; // S

        // WEST = W E S T → indexes 9 5 7 3
        if (disallowedValues_w.Contains(arr[9])) return false; // W

        // SOUTH = S O U T H → indexes 7 1 8 3 4
        if (disallowedValues_u.Contains(arr[8])) return false; // U

        return true; // Passed all checks
    }


    static void Permute(int[] arr, int start, int end)
    {
        if (start == end)
        {
            if (!IsValidCombination(arr)) return;

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