using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;


public class UnityCarUserController : VehicleController
{
    private Vehicle vehicle;

    public UnityCarUserController()
    {

    }

    public void InitController(Vehicle vehicle)
    {
        this.vehicle = vehicle;
    }

    public void ResetVehicleController()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateController()
    {
        // pass the input to the car!
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
        float handbrake = CrossPlatformInputManager.GetAxis("Jump");
        vehicle.SetVehicleInput(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
    }
}
