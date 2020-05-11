using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract environment master allows as to expose environment masters to the editor as interfaces cannot be used for this purpose.
public abstract class BaseEnvironmentMaster : MonoBehaviour, EnvironmentMaster
{
    public abstract AbstractDummyCarController GetDummyCarController();

    public abstract void InitEnvironmentMaster();

    public abstract void UpdateEnvironmentMaster();
}
