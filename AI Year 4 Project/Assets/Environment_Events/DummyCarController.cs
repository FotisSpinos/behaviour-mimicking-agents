using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A vehicle controller following a fixed instructions. To be used by the dummy car in the training scene 
 */

public class DummyCarController : VehicleController
{
    private Vehicle vehicle;

    private int stateIndex;

    private XML_Manager xml;


    public delegate void Reset();
    public event Reset OnReset;

    public void InitController(Vehicle vehicle)
    {
        this.vehicle = vehicle;

        stateIndex = 0;

        xml = new XML_Manager();
        xml.Load(Files.DummyCar);
    }

    public void UpdateController()
    {
        if (stateIndex < xml.agentStates.states.Count)
        {
            stateIndex++;

            vehicle.GetGameObject().transform.localPosition = xml.agentStates.states[stateIndex - 1].position;
            vehicle.GetGameObject().transform.eulerAngles = xml.agentStates.states[stateIndex - 1].rotation;
            
        }
        else
        {
            ResetVehicleController();

            if(OnReset != null)
                OnReset();
        }
    }

    public void ResetVehicleController()
    {
        int randIndex = Random.Range(0, xml.agentStates.states.Count - 1);
        stateIndex = (randIndex);

        // update controller once a new index is being set
        UpdateController();
    }

    public Vehicle GetVehicle()
    {
        return vehicle;
    }

    public Vector3 GetCurrentVelocity()
    {
        return xml.agentStates.states[stateIndex - 1].velocity;
    }
}
