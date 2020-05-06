using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class UnityCar : MonoBehaviour, Vehicle
{
    [SerializeField] private CarController m_Car;

    public UnityCar() { }

    public GameObject GetGameObject()
    {
        throw new System.NotImplementedException();
    }

    public float GetMaxSpeed()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetPosition()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetVelocity()
    {
        throw new System.NotImplementedException();
    }

    public void ImpactRecorded()
    {
        throw new System.NotImplementedException();
    }

    public bool IsColliding()
    {
        throw new System.NotImplementedException();
    }

    public void SetVehicleInput(params float[] parameters)
    {
        if (parameters != null || parameters.Length == 4)
        {
            m_Car.Move(parameters[0], parameters[1], parameters[2], parameters[3]);
        }
    }
}
