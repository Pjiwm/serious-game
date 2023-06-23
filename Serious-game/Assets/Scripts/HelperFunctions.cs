using System;
using UnityEngine;

public static class HelperFunctions
{
    /// <summary><para>Returns the distance between two vectors. Order of input is irrelevant</para></summary>
    public static float GetDistanceToVector(Vector2 vectorA, Vector2 vectorB)
    {
        return Math.Abs((vectorA - vectorB).magnitude);
    }
}