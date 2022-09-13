
// Problem 1:
// Longest Increasing Sub-Sequence and Length of Longest Increasing Sub-Sequence


// a)   Implementing

void LISR(int[] array, List<List<int>> table, int i = 0)
{
    if (i >= array.Length) return;

    for (int j = 0; j < i; j++)
    {
        if ((array[i] > array[j]) && (table[i].Count < table[j].Count + 1))
            table[i] = table[j];
    }

    table[i].Add(array[i]);
    LISR(array, table, ++i);
    return;
}

int[] FindLISS_Recursive(int[] array)
{
    List<List<int>> table = new();
    for (int i = 0; i < array.Length; i++)
    {
        table.Add(new());
    }

    LISR(array, table);

    var lis = table[0];
    foreach (var list in table)
    {
        if (list.Count > lis.Count)
        {
            lis = list;
        }
    }
    return lis.ToArray();
}

int[] FindLISS_DynamicProgramming(int[] array)
{
    List<int> subsequence = Enumerable.Repeat(0, array.Length).ToList();
    List<int> lengthArray = Enumerable.Repeat(1, array.Length).ToList();

    for (int i = 0; i < array.Length; i++)
    {
        for (int j = 0; j < i; j++)
        {
            if (array[j] < array[i])
            {
                lengthArray[i] = Math.Max(lengthArray[i], 1 + lengthArray[j]);
                if (lengthArray[i] <= lengthArray[j] + 1)
                    subsequence[i] = j;

            }
        }
    }

    List<int> longestSequence = new List<int>();
    int index = lengthArray.IndexOf(lengthArray.Max());
    while (index != 0)
    {
        longestSequence.Add(array[index]);
        index = subsequence[index];
    }
    if (array[index] < longestSequence[longestSequence.Count - 1])
        longestSequence.Add(array[index]);
    longestSequence.Reverse();

    return longestSequence.ToArray();
}

int LengthLISR(int[] array, int current, int previous)
{
    if (current == array.Length) return 0;

    int firstLength = 0;
    if (previous < 0 || array[previous] < array[current])
    {
        firstLength = 1 + LengthLISR(array, current + 1, current);
    }
    int secondLength = LengthLISR(array, current + 1, previous);

    return Math.Max(firstLength, secondLength);
}

int FindLengthOfLISS_Recursive(int[] array)
{
    return LengthLISR(array, 0, -1);
}

int FindLengthOfLISS_DynamicProgramming(int[] array)
{
    int[] lengthArray = Enumerable.Repeat(1, array.Length).ToArray();

    for (int i = 0; i < array.Length; i++)
    {
        for (int j = 0; j < i; j++)
        {
            if (array[j] < array[i])
            {
                lengthArray[i] = Math.Max(lengthArray[i], 1 + lengthArray[j]);
            }
        }
    }

    return lengthArray.Max();
}


// b)   Testing
int[] GenerateRandomArray(int amount)
{
    Random random = new Random();

    int[] array = new int[amount];

    for (int i = 0; i < array.Length; i++)
    {
        array[i] = random.Next(1, 256);
    }

    return array;
}

int[] array1 = { 69, 21, 201, 91, 30, 20, 33, 3, 80, 19, 76, 21, 10, 11, 2, 44, 9 };
int[] array2 = { 29, 21, 51, 60, 84, 24, 44, 8, 59, 63, 76, 69, 83, 89, 52, 2 };
int[] array3 = GenerateRandomArray(100);

// c)   Benchmarking

Console.WriteLine("Longest Increasing Sub-Sequence:\n");

{
    using var timer = new Benchmark("Recursive (array1)");
    FindLISS_Recursive(array1);
}

{
    using var timer = new Benchmark("Recursive (array2)");
    FindLISS_Recursive(array2);
}

{
    using var timer = new Benchmark("Recursive (array3)");
    FindLISS_Recursive(array3);
}

{
    using var timer = new Benchmark("Dynamic Programming (array1)");
    FindLISS_DynamicProgramming(array1);
}

{
    using var timer = new Benchmark("Dynamic Programming (array2)");
    FindLISS_DynamicProgramming(array2);
}

{
    using var timer = new Benchmark("Dynamic Programming (array3)");
    FindLISS_DynamicProgramming(array3);
}

Console.WriteLine("\nLength of Longest Increasing Sub-Sequence:\n");

{
    using var timer = new Benchmark("Recursive (array1)");
    FindLengthOfLISS_Recursive(array1);
}

{
    using var timer = new Benchmark("Recursive (array2)");
    FindLengthOfLISS_Recursive(array2);
}

{
    using var timer = new Benchmark("Recursive (array3)");
    FindLengthOfLISS_Recursive(array3);
}

{
    using var timer = new Benchmark("Dynamic Programming (array1)");
    FindLengthOfLISS_DynamicProgramming(array1);
}

{
    using var timer = new Benchmark("Dynamic Programming (array2)");
    FindLengthOfLISS_DynamicProgramming(array2);
}

{
    using var timer = new Benchmark("Dynamic Programming (array3)");
    FindLengthOfLISS_DynamicProgramming(array3);
}


// Problem 2
// Floyd's Algorithm
//     a   b   c   d
// a [ *   2   5   * ]
// b [ 9   *   8   * ]
// c [ 8   *   *   7 ]
// d [ 1   8   9   * ]

int inf = 1000000000;

int[][] matrix = new int[][]
{
    new[]{ inf, 2, 5, inf },
    new[]{ 9, inf, 8, inf },
    new[]{ 8, inf, inf, 7 },
    new[]{ 1, 8, 9, inf },
};

int[][] test = new int[][]
{
    new[]{ inf, 3, inf, 7 },
    new[]{ 8, inf, 2, inf },
    new[]{ 5, inf, inf, 1 },
    new[]{ 2, inf, inf, inf },
};

int[][] ShortestPath(int[][] mat)
{
    int[][] shortest = mat;
    for (int i = 0; i < shortest.Length; i++)
    {
        shortest[i][i] = 0;
    }

    for (int k = 0; k < mat.Length; k++)
    {
        for (int i = 0; i < mat.Length; i++)
        {
            for (int j = 0; j < mat.Length; j++)
            {
                shortest[i][j] = Math.Min(shortest[i][j], shortest[i][k] + shortest[k][j]);
            }
        }
    }

    return shortest;
}

Console.WriteLine("\nFloyd's Algorithm\n");

Console.WriteLine($"0 2 5 ∞");
Console.WriteLine($"9 0 8 ∞");
Console.WriteLine($"8 ∞ 0 7");
Console.WriteLine($"1 8 5 0");

Console.WriteLine();
Console.WriteLine("   |");
Console.WriteLine("   V");
Console.WriteLine();

var shortest = ShortestPath(matrix);
for (int i = 0; i < shortest.Length; i++)
{
    for (int j = 0; j < shortest.Length; j++)
    {
        Console.Write(shortest[i][j] + " ");
    }
    Console.WriteLine();
}