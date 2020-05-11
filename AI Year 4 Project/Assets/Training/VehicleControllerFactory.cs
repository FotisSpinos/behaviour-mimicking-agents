using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VehicleControllerFactory
{
    public static AbstractDummyCarController CreateDummyCar(string dummyCarKey)
    {
        switch(dummyCarKey)
        {
            case "SIMPLE_DUMMY_CAR_CONTROLLER":
                return new SimpleDummyCarController();
            case "UNITY_DUMMY_CAR_CONTROLLER":
                return new UnityDummyCarController();
            default:
                Debug.LogWarning("Unsuported key: " + dummyCarKey);
                break;
        }
        return null;
    }

    public static VehicleController CreateUserVehicleController(string userContorllerKey)
    {
        switch(userContorllerKey)
        {
            case "USER_VEHICLE_CONTROLLER":
                return new UserVehicleController();
            case "UNITY_CAR_VEHICLE_CONTROLLER":
                return new UserUnityVehicleController();
            default:
                Debug.LogWarning("Unsuported key: " + userContorllerKey);
                break;
        }        
        return null;
    }
}