using UnityEngine;

public static class Bezier 
{
        
    public static Vector3  GetBezier(Vector3 p0, Vector3 p1,Vector3 p2,Vector3 p3,float t)
    {
        t = Mathf.Clamp01(t);
        float OneT = 1f - t;
        return
            OneT * OneT * OneT * p0 +
            3f * OneT * OneT * t * p1 +
            3f * OneT * t * t * p2 +
            t * t * t * p3;

    }

    public static Vector3 GetBezierRotation(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float OneT = 1 - t;
        return 3f * OneT * OneT * (p0 - p1) +
               6f * OneT *  t * (p2 - p1) +
               3f * t * t * (p3 - p2);

    }
}