using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A vehicle controller following a fixed instructions. To be used by the dummy car in the training scene 
 */

public class TrainingVehicleController : VehicleController
{
    private Vehicle vehicle;

    private int stateIndex;

    XML_Manager xml;

    public TrainingVehicleController()
    {
    }

    public void InitController(Vehicle vehicle)
    {
        this.vehicle = vehicle;
        stateIndex = 0;

        xml = new XML_Manager();
        xml.Load();
    }

    public void UpdateController()
    {
        if(stateIndex < xml.agentStates.states.Count)
        {
            vehicle.GetGameObject().transform.position = xml.agentStates.states[stateIndex].position;
            vehicle.GetGameObject().transform.eulerAngles = xml.agentStates.states[stateIndex].rotation;
            stateIndex++;
        }
        else
        {
            stateIndex = 0;
        }
    }
}
