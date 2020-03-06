using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnvironmentMaster : MonoBehaviour, EnvironmentMaster
{
    public virtual void InitEnvironmentMaster()
    {
        Debug.LogError("InitEnvironmentMaster is called, no functionality is provided");
    }

    public virtual void UpdateEnvironmentMaster()
    {
        Debug.LogError("UpdateEnvironmentMaster is called, no functionality is provided");
    }
}
