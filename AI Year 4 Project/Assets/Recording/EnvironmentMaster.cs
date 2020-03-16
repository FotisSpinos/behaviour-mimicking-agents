using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnvironmentMaster
{
    void InitEnvironmentMaster();

    void UpdateEnvironmentMaster();

    DummyCarController GetDummyCarController();
}
