using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Normalize
{
    public static float NormNum(float num, float min, float max)
    {
        return (num - min) / (max - min);
    }

    public static float NormNum(float num, float range)
    {
        range = Mathf.Abs(range);

        return NormNum(num, -range, range);
    }

    public static Quaternion NormQuat(Quaternion quat)
    {
        quat.x = NormNum(quat.x, -1.0f, 1.0f);
        quat.y = NormNum(quat.y, -1.0f, 1.0f);
        quat.z = NormNum(quat.z, -1.0f, 1.0f);
        quat.w = NormNum(quat.w, -1.0f, 1.0f);
        return quat;
    }

    public static Vector3 NormVec(Vector3 num, Vector3 min, Vector3 max)
    {
        num.x = NormNum(num.x, min.x, max.x);
        num.y = NormNum(num.y, min.y, max.y);
        num.z = NormNum(num.z, min.z, max.z);
        return num;
    }

    public static Vector3 NormVec(Vector3 num, Vector3 range)
    {
        range.x = Mathf.Abs(range.x);
        range.y = Mathf.Abs(range.y);
        range.z = Mathf.Abs(range.z);

        num.x = NormNum(num.x, -range.x, range.x);
        num.y = NormNum(num.y, -range.y, range.y);
        num.z = NormNum(num.z, -range.z, range.z);
        return num;
    }
}

