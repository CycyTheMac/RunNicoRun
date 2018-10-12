using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDraw : MonoBehaviour {

    public static Color color= Color.green;
    public static float duration=1f;

    
    public static void Path(List<Transform> path)
    {
        Path(path.ToArray());

    }
    public static void Path(Transform[] path) {
        List<Vector3> points = new List<Vector3>();
        foreach (Transform point in path)
        {
            points.Add(point.position);
        }
        Path(points);
    }

    internal static void Cartesian(Vector3 position, Quaternion rotation, float lenght = 1, Space type = Space.Self)
    {
        Debug.DrawLine(position, position + (rotation * Vector3.forward) * lenght, Color.blue, duration);
        Debug.DrawLine(position, position + (rotation * Vector3.right) * lenght, Color.red, duration);
        Debug.DrawLine(position, position + (rotation * Vector3.up) * lenght, Color.green, duration);
    }
    internal static void Cross(Vector3 position, Quaternion rotation, float lenght = 1, Space type = Space.Self)
    {
        Debug.DrawLine(position, position + (rotation * Vector3.forward) * lenght, Color.blue, duration);
        Debug.DrawLine(position, position + (rotation * Vector3.right) * lenght, Color.red, duration);
        Debug.DrawLine(position, position + (rotation * Vector3.up) * lenght, Color.green, duration);
        Debug.DrawLine(position, position + (rotation * Vector3.back) * lenght, Color.blue, duration);
        Debug.DrawLine(position, position + (rotation * Vector3.left) * lenght, Color.red, duration);
        Debug.DrawLine(position, position + (rotation * Vector3.down) * lenght, Color.green, duration);
    }
    internal static void Cross(Vector3 position, Quaternion rotation, Color color , float lenght = 1, Space type = Space.Self)
    {
        Debug.DrawLine(position, position + (rotation * Vector3.forward) * lenght, color, duration);
        Debug.DrawLine(position, position + (rotation * Vector3.right) * lenght, color, duration);
        Debug.DrawLine(position, position + (rotation * Vector3.up) * lenght, color, duration);
        Debug.DrawLine(position, position + (rotation * Vector3.back) * lenght, color, duration);
        Debug.DrawLine(position, position + (rotation * Vector3.left) * lenght, color, duration);
        Debug.DrawLine(position, position + (rotation * Vector3.down) * lenght, color, duration);
    }

    public static void Path(List<Vector3> path)
    {
        if (path.Count <= 1) return;
        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i], path[i + 1], color, duration);
        }
    }
    
}
