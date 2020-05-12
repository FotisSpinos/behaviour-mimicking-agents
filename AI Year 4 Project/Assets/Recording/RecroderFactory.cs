using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RecroderFactory
{
    public static AbstractRecorder CreateRecorder(string userControllerKey, AbstractVehicle abstractVehicle, float fixedTimeInterval)
    {
        switch(userControllerKey)
        {
            case "USER_VEHICLE_CONTROLLER":
                return new StateRecorder(abstractVehicle, fixedTimeInterval);
            case "UNITY_CAR_VEHICLE_CONTROLLER":
                return new UnityCarStateRecorder((UnityVehicle) abstractVehicle, fixedTimeInterval);
            default:
                Debug.LogWarning("Unsuported key: " + userControllerKey);
                break;
        }        
        return null;
    }
}
