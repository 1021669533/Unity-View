namespace UnityView.Collections
{
    public class Algorithm
    {
        public static int BinarySearchFloat(float[] sortedArray, float value)
        {
            return BinarySearchFloat(sortedArray, 0, sortedArray.Length, value);
        }

        public static int BinarySearchFloat(float[] sortedArray, int start, int end, float value)
        {
            if (start == end) return start;
            if (value < sortedArray[start]) return -1;
            while (end - start != 1)
            {
                var current = (end + start) >> 1;
                if (value >= sortedArray[current])
                {
                    start = current;
                }
                else
                {
                    end = current;
                }
            }
            return start;
        }
    }
}
