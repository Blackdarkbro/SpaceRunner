using System.Collections.Generic;

public static class Utility
{
    public static int EvenSum(IEnumerable<int> coll)
    {
        var sum = 0;
        foreach (var elem in coll)
            if (elem % 2 == 0)
                sum += elem;

        return sum;
    }
}