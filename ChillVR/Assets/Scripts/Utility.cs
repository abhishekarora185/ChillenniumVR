
using UnityEngine;

public static class Utility {
    
    public static bool AreFloatsEqual(float a, float b)
    {
        float epsilon = 0.001f;
        float difference = Mathf.Abs(a - b);

        if (difference <= epsilon)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
