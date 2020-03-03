using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingVehicleController : VehicleController
{
    private Vehicle vehicle;

    private int stateIndex;

    private List<Vector3> positions;
    private List<Vector3> rotations;
    private List<Vector3> velocity;

    public TrainingVehicleController()
    {
    }

    public void InitController(Vehicle vehicle)
    {
        this.vehicle = vehicle;
        stateIndex = 0;

        positions = new List<Vector3>();
        rotations = new List<Vector3>();

        // FAKE DATA FOR TESTING - REMOVE LATER!!
        positions.Add(new Vector3(1.0f, 0.0f, 0.0f));
        positions.Add(new Vector3(2.0f, 0.0f, 0.0f));
        positions.Add(new Vector3(3.0f, 0.0f, 0.0f));
        positions.Add(new Vector3(4.0f, 0.0f, 0.0f));
        positions.Add(new Vector3(5.0f, 0.0f, 0.0f));
        positions.Add(new Vector3(6.0f, 0.0f, 0.0f));

        rotations.Add(new Vector3(10.0f, 0.0f, 0.0f));
        rotations.Add(new Vector3(20.0f, 0.0f, 0.0f));
        rotations.Add(new Vector3(30.0f, 0.0f, 0.0f));
        rotations.Add(new Vector3(40.0f, 0.0f, 0.0f));
        rotations.Add(new Vector3(50.0f, 0.0f, 0.0f));
        rotations.Add(new Vector3(60.0f, 0.0f, 0.0f));
    }

    public void UpdateController()
    {
        if(stateIndex < positions.Count)
        {
            vehicle.GetGameObject().transform.position = positions[stateIndex];
            vehicle.GetGameObject().transform.rotation = Quaternion.Euler(rotations[stateIndex]);
            stateIndex++;
        }
        else
        {
            stateIndex = 0;
        }
    }
}
