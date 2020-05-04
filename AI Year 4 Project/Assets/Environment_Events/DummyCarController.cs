using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A vehicle controller following a fixed instructions. To be used by the dummy car in the training scene 
 */

public class DummyCarController : VehicleController
{
    private Vehicle vehicle;

    private int animIndex;
    private int stateIndex;

    private bool enableRandomAnimIndex;

    public bool EnableRandomAnimIndex
    {
        set
        {
            enableRandomAnimIndex = value;
        }
    }


    public delegate void Reset();
    public event Reset OnReset;

    public DummyCarController()
    {
        enableRandomAnimIndex = true;
    }

    public void InitController(Vehicle vehicle)
    {
        this.vehicle = vehicle;
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
        if (StatesManager.GetInstance().GetSerializableAgentStates().Count == 0)
            return;

        // choose random animation
        animIndex = Random.Range(0, StatesManager.GetInstance().GetSerializableAgentStates().Count - 1);

        if (enableRandomAnimIndex)
        {
            // choose random point in the selected animation
            stateIndex = Random.Range(0, StatesManager.GetInstance().GetSerializableAgentState(animIndex).states.Count - 1);
        }
        else
        {
            stateIndex = 0;
        }

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
