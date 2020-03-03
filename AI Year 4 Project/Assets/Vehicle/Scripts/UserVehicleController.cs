using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserVehicleController : MonoBehaviour, VehicleController
{
    private Vehicle vehicle;

    public UserVehicleController(Vehicle vehicle)
    {
        this.vehicle = vehicle;
    }

    public void InitController(Vehicle vehicle)
    {
        
    }

    public void UpdateController()
    {
        vehicle.SetVehicleInput(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
    }
}
