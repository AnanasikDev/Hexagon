using UnityEngine;

public static class Extensions
{
    public static bool Multiples(this int a, int b)
    {
        return b != 0 && a % b == 0 && a != 0;
    }
    
}