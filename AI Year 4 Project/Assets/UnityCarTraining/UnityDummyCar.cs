using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class UnityDummyCarController : AbstractDummyCarController
{
    private UnityVehicle vehicle;

    public override void InitController(AbstractVehicle vehicle)
    {
        //this.vehicle = vehicle;
        this.vehicle = (UnityVehicle) vehicle;
    }

    public UnityDummyCarController(){}

    public override void UpdateController()
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
            OnResetOccured();
        }
    }

    public override void ResetVehicleController()
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

    public override AbstractVehicle GetVehicle()
    {
        return vehicle;
    }

    public CarController.Atributes GetCurrentAtribute()
    {
        return LoadedVehicleStates.GetInstance().GetSerCarControllerAtrs()[animIndex].carContrAtr[stateIndex - 1];
    }

    public override Vector3 GetCurrentVelocity()
    {
        //return LoadedVehicleStates.GetInstance().GetSerCarControllerAtrs()[animIndex].carContrAtr[stateIndex - 1].;
        return Vector3.zero;
    }
}