using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

public class UserUnityVehicleController : VehicleController
{
    private AbstractVehicle vehicle;

    public UserUnityVehicleController()
    {

    }

    public void InitController(AbstractVehicle vehicle)
    {
        this.vehicle = vehicle;
    }

    public void ResetVehicleController()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateController()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
        float handbrake = CrossPlatformInputManager.GetAxis("Jump");
        vehicle.SetVehicleInput(h, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
    }
}
