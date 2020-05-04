using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Vehicle
{
    void SetVehicleInput(float steeringAmount, float speed);

    void Steer(float steeringAmount);

    void Move(float forwardAmount);

    Vector3 GetVelocity();

    Vector3 GetPosition();

    float GetMaxSpeed();

    GameObject GetGameObject();

    bool IsColliding();

    void ImpactRecorded();
}
