using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractVehicle : MonoBehaviour, Vehicle
{
    public AbstractVehicle() { }

    public abstract GameObject GetGameObject();

    public abstract float GetMaxSpeed();

    public abstract Vector3 GetPosition();

    public abstract Vector3 GetVelocity();

    public abstract void ImpactRecorded();

    public abstract bool IsColliding();

    public abstract void SetVehicleInput(params float[] list);

    public abstract void SetVeolcity(Vector3 velocity);
}
