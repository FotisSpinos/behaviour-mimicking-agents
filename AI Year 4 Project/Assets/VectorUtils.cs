using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorUtils : MonoBehaviour
{
    public static Vector3 CreateRandomUnitVector()
    {
        Vector3 output = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f),
         UnityEngine.Random.Range(-1.0f, 1.0f),
         UnityEngine.Random.Range(-1.0f, 1.0f));

        return output.normalized;
    }

    public static Vector3 CreateRandomVector(float minMagnitude, float maxMagnitude)
    {
        float randMagnitude = UnityEngine.Random.Range(minMagnitude, maxMagnitude);
        Vector3 randVec = CreateRandomUnitVector();

        return randVec.normalized * randMagnitude;
    }
}
