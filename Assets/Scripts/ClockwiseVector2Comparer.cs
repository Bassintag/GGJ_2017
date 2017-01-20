using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockwiseVector2Comparer : IComparer<Vector2> {

    public Vector2 _origin;

    public ClockwiseVector2Comparer(Vector2 origin)
    {
        _origin = origin;
    }
    
    public int Compare(Vector2 v1, Vector2 v2)
    {
        return (IsClockwise(v1, v2, _origin));
    }

    public static int IsClockwise(Vector2 first, Vector2 second, Vector2 origin)
    {
        if (first == second)
            return 0;
        Vector2 firstOffset = first - origin;
        Vector2 secondOffset = second - origin;
        float angle1 = Mathf.Atan2(firstOffset.x, firstOffset.y);
        float angle2 = Mathf.Atan2(secondOffset.x, secondOffset.y);
        if (angle1 < angle2)
            return 1;
        if (angle1 > angle2)
            return -1;
        return (firstOffset.sqrMagnitude < secondOffset.sqrMagnitude) ? 1 : -1;
    }
}
