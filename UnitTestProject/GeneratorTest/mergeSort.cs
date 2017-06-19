public class sort{

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

        for (i = 0; i < N - 1; i++)
        {
            int k = IntArrayMin(data, i, size);
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