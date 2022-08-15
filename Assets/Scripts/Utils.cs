using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class Utils
{
    public const float C = 50f; // Speed of light
    public const float G = 100f;
    public static readonly float ReducedPlank = 1.055f * Mathf.Pow(10, -34);

    public static Vector2 FromAngle(float theta, float length = C)
    {
        return new Vector2(length * Mathf.Cos(theta), length * Mathf.Sin(theta));
    }
}
