using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A vehicle controller following a fixed instructions. To be used by the dummy car in the training scene 
 */

public class DummyCarController : VehicleController
{
    private Vehicle vehicle;

    private XmlReadWrite xmlManager;

    private int animIndex;
    private int stateIndex;

    public delegate void Reset();
    public event Reset OnReset;

    public void InitController(Vehicle vehicle)
    {
        this.xmlManager = XmlReadWrite.GetInstance();
        this.vehicle = vehicle;
    }

    public void SetXmlManager(XmlReadWrite xmlManager)
    {
        this.xmlManager = xmlManager;
    }

    public void UpdateController()
    {
        int endAnimIndex = StatesManager.GetInstance().GetSerializableAgentState(animIndex).states.Count;

        if (stateIndex < endAnimIndex)
        {
            stateIndex++;

            AgentState currentState = StatesManager.GetInstance().GetSerializableAgentState(animIndex).states[stateIndex - 1];

            // apply the animation state to the agent object
            vehicle.GetGameObject().transform.localPosition = currentState.position;
            vehicle.GetGameObject().transform.eulerAngles = currentState.rotation;            
        }
        else
        {
            ResetVehicleController();

            OnReset?.Invoke();
        }
    }

    public void ResetVehicleController()
    {
        // choose random animation
        animIndex = Random.Range(0, StatesManager.GetInstance().GetSerializableAgentStates().Count - 1);

        // choose random point in the selected animation
        stateIndex = Random.Range(0, StatesManager.GetInstance().GetSerializableAgentState(animIndex).states.Count - 1);
        
        // update controller once a new index is being set
        UpdateController();
    }

    public Vehicle GetVehicle()
    {
        return vehicle;
    }

    public Vector3 GetCurrentVelocity()
    {
        return StatesManager.GetInstance().GetSerializableAgentState(animIndex).states[stateIndex - 1].velocity;
    }
}
