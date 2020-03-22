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
    //[Range(0,4)]public int pathIndex;
    public Files pathFile;
    private XML_Manager xml = GameObject.Find("Obj_GameManager").GetComponent<XML_Manager>();
    private AgentStates path;

    public delegate void Reset();
    public event Reset OnReset;

    public void InitController(Vehicle vehicle)
    {
        this.vehicle = vehicle;

        stateIndex = 0;

        // = new XML_Manager();
        //xml.Load(Files.CarPath_2);

        path = xml.pathsList[(int)pathFile];
    }

    public void UpdateController()
    {
        //if (stateIndex < xml.agentStates.states.Count)
        if (stateIndex < path.states.Count)
        {
            stateIndex++;

            //vehicle.GetGameObject().transform.localPosition = xml.agentStates.states[stateIndex - 1].position;
            //vehicle.GetGameObject().transform.eulerAngles = xml.agentStates.states[stateIndex - 1].rotation;
                        
            
            vehicle.GetGameObject().transform.localPosition = path.states[stateIndex - 1].position;
            vehicle.GetGameObject().transform.eulerAngles = path.states[stateIndex - 1].rotation;
            
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
        //int randIndex = Random.Range(0, xml.agentStates.states.Count - 1);
        int randIndex = Random.Range(0, path.states.Count - 1);
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
        //return xml.agentStates.states[stateIndex - 1].velocity;
        return path.states[stateIndex - 1].velocity;
    }
}
