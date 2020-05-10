using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class UnityDummyCar : VehicleController
{
    private UnityVehicle vehicle;

    private int animIndex;
    private int stateIndex;

    private bool enableRandomAnimIndex;

    public delegate void Reset();
    public event Reset OnReset;

    public bool EnableRandomAnimIndex
    {
        set
        {
            enableRandomAnimIndex = value;
        }
    }

    public UnityDummyCar(UnityVehicle vehicle)
    {
        this.vehicle = vehicle;
    }

    public void InitController(Vehicle vehicle)
    {
    }

    public void UpdateController()
    {
        int endAnimIndex = LoadedVehicleStates.GetInstance().GetSerCarControllerAtrs()[animIndex].carContrAtr.Count;

        if (stateIndex < endAnimIndex)
        {
            stateIndex++;
            CarController.Atributes currentState = LoadedVehicleStates.GetInstance().GetSerCarControllerAtrs()[animIndex].carContrAtr[stateIndex - 1];
            vehicle.CarController.SetCarControllerAtr(currentState);
        }
        else
        {
            ResetVehicleController();
            OnReset?.Invoke();
        }
    }

    public void ResetVehicleController()
    {
        if (LoadedVehicleStates.GetInstance().GetSerCarControllerAtrs().Count == 0)
            return;

        // choose random animation
        animIndex = Random.Range(0, LoadedVehicleStates.GetInstance().GetSerCarControllerAtrs().Count - 1);

        if (enableRandomAnimIndex)
        {
            // choose random point in the selected animation
            stateIndex = Random.Range(0, LoadedVehicleStates.GetInstance().GetSerCarControllerAtrs()[animIndex].carContrAtr.Count - 1);
        }
        else
        {
            stateIndex = 0;
        }

        // update controller once a new index is being set
        UpdateController();
    }

    public Vehicle GetVehicle()
    {
        return vehicle;
    }

    public CarController.Atributes GetCurrentAtribute()
    {
        return LoadedVehicleStates.GetInstance().GetSerCarControllerAtrs()[animIndex].carContrAtr[stateIndex - 1];
    }

    public Vector3 GetCurrentVelocity()
    {
        //return LoadedVehicleStates.GetInstance().GetSerCarControllerAtrs()[animIndex].carContrAtr[stateIndex - 1].;
        return Vector3.zero;
    }
}