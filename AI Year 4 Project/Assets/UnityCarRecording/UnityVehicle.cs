using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class UnityVehicle : AbstractVehicle
{
    [SerializeField] private CarController carController;

    public CarController CarController
    {
        get
        {
            return carController;
        }
    }

    public UnityVehicle() { }

    public override GameObject GetGameObject()
    {
        return carController.gameObject;
    }

    public override float GetMaxSpeed()
    {
        throw new System.NotImplementedException();
    }

    public override Vector3 GetPosition()
    {
        throw new System.NotImplementedException();
    }

    public override Vector3 GetVelocity()
    {
        throw new System.NotImplementedException();
    }

    public override void ImpactRecorded()
    {
        //throw new System.NotImplementedException();
    }

    public override bool IsColliding()
    {
        return false;
        //throw new System.NotImplementedException();
    }

    public override void SetVehicleInput(params float[] parameters)
    {
        if (parameters != null && parameters.Length == 3)
        {
            carController.Move(parameters[0], parameters[1], parameters[1], parameters[2]);
        }
        else
        {
            Debug.LogError("Invalid number of inputs passed to the Unity Vehicle " + gameObject.name);
        }
    }

    public override void SetVeolcity(Vector3 velocity)
    {
        
    }
}
