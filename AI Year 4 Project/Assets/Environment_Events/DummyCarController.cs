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

    public Files pathFile;
    private XML_Manager xmlManager;

    public delegate void Reset();
    public event Reset OnReset;

    public void InitController(Vehicle vehicle)
    {
        this.vehicle = vehicle;

        stateIndex = 0;

        this.xmlManager = XML_Manager.GetInstance();
    }

    public void SetPathFile(Files pathFile)
    {
        this.pathFile = pathFile;
    }

    public void SetXmlManager(XML_Manager xmlManager)
    {
        this.xmlManager = xmlManager;
    }

    private AgentStates CurrentPathList()
    {
        return xmlManager.pathsList[(int)pathFile];
    }

    public void UpdateController()
    {
        if(xmlManager == null)
        {
            Debug.Log("xml manager is undefined in dummy car controller");
            return;
        }

        if (stateIndex < CurrentPathList().states.Count)
        {
            stateIndex++;

            vehicle.GetGameObject().transform.localPosition = CurrentPathList().states[stateIndex - 1].position;
            vehicle.GetGameObject().transform.eulerAngles = CurrentPathList().states[stateIndex - 1].rotation;            
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
        // choose random animation
        pathFile = (Files)Random.Range(0, 5);

        // choose random point in the selected animation
        int randIndex = Random.Range(0, CurrentPathList().states.Count - 1);
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
        return CurrentPathList().states[stateIndex - 1].velocity;
    }
}
