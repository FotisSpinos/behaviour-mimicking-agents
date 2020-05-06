using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Vehicle
{
    void SetVehicleInput(params float[] list);

    Vector3 GetVelocity();

    Vector3 GetPosition();

    float GetMaxSpeed();

    GameObject GetGameObject();

    bool IsColliding();

    void ImpactRecorded();
}
