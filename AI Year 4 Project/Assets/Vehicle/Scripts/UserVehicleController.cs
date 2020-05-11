using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserVehicleController : MonoBehaviour, VehicleController
{
    private AbstractVehicle vehicle;

    public void InitController(AbstractVehicle vehicle)
    {
        this.vehicle = vehicle;
    }

    public void ResetVehicleController()
    {
        
    }

    public void UpdateController()
    {
        vehicle.SetVehicleInput(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
    }
}
