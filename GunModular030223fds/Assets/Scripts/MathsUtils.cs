using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathsUtils
{
    public static float IncreaseFloatByPercentage(float Current, float Increase)
    {
        float i = (Increase / 100) * Current;
        return Current + i;
    }

    public static float DecreaseFloatByPercentage(float Current, float Increase)
    {
        float i = (Increase / 100) / Current;
        return Current - i;
    }
    public enum eThrowTypes
    {
        Height, HighSpeed, LowSpeed
    }
    
    public static bool CalculateTrajectory(Vector3 start, Vector3 target, float topSpeed, eThrowTypes throwType, out Vector3 velocity, out float flightTime)
    {
        Vector3 startEndDiff = target - start;
        float gSquared = Physics.gravity.sqrMagnitude;
        float b = topSpeed * topSpeed + Vector3.Dot(startEndDiff, Physics.gravity);
        float discriminant = b * b - gSquared * startEndDiff.sqrMagnitude;

        if (discriminant < 0)
        { 
            //Wont hit:
            velocity = Vector3.zero;
            flightTime = 0.0f;
            return false;
        }

        float discRoot = Mathf.Sqrt(discriminant);

        switch (throwType)
        {
            case eThrowTypes.Height:
                flightTime = Mathf.Sqrt((b + discRoot) * 2f / gSquared);
                break;
            case eThrowTypes.HighSpeed:
                flightTime = Mathf.Sqrt((b - discRoot) * 2f / gSquared);
                break;
            case eThrowTypes.LowSpeed:
                flightTime = Mathf.Sqrt((b + discRoot) * 2f / gSquared);
                break;
            default:
                flightTime = 0.5f;
                break;
        }

        velocity = startEndDiff / flightTime - Physics.gravity * flightTime / 2.0f;
        return true;
    }

    /// <summary>
    /// Gets random point on edge of unit circle returned as Vector2
    /// </summary>
    /// <param name="radius">Radius of the circle</param>
    /// <returns>Random point on unit circle edge as Vector2</returns>
    public static Vector2 RandomPointOnUnitCircle(float radius)
    {
        float angle = Random.Range(0.0f, Mathf.PI * 2);
        float x = Mathf.Sin(angle) * radius;
        float y = Mathf.Cos(angle) * radius;

        return new Vector2(x, y);
    }
}
