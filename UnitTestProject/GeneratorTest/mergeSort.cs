using System;
public class sort{
    
    public static void Main(string[] args)
    {
        int[] array = { 7,50,20,40,90,6,4 };
        int size = 7;
        IntArraySelectionSort(array, size);
        for (int i = 0; i < size; i++) {
            Console.WriteLine(""+array[i]);
        }
    }

    public static int IntArrayMin(int[] data, int start, int size)
    {
        int minPos = start;
        for (int pos = start + 1; pos < size; pos++)
            if (data[pos] < data[minPos])
                minPos = pos;
        return minPos;
    }

    public static void IntArraySelectionSort(int[] data, int size)
    {
        int i;
        int N = size;
        Console.WriteLine("Hola");
        int n = int.Parse("5");
        for (i = 0; i < N - 1; i++)
        {
            int k = this.IntArrayMin(data, i, size);
            if (i != k)
                exchange(data, i, k);
        }
    }
    public static void exchange(int[] data, int m, int n)
    {
        int temporary;

        temporary = data[m];
        data[m] = data[n];
        data[n] = temporary;
    }                  
}