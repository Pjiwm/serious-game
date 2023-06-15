using System;
using UnityEngine;

public static class HelperFunctions
{
    public static Vector3 ToVector3(this Vector2 vector)
    {
        return vector;
    }

    /// <summary><para>Returns the distance between two vectors. Order of input is irrelevant</para></summary>
    public static float GetDistanceToVector(Vector2 vectorA, Vector2 vectorB)
    {
        return Math.Abs((vectorA - vectorB).magnitude);
    }
}