using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MLAgents;

// we need a decision requester component
// we need behaviour parameters component

public class AgentCar : Agent
{
    private Vehicle car;
    [SerializeField] private GameObject dummyCar;

    private void Start()
    {
        // add car vehicle to this game object
        car = gameObject.GetComponent<CarVehicle>();    
        
    }

    public override void AgentAction(float[] vectorAction)
    {
        // apply actions
        float forwardAmount = vectorAction[0];
        float steerAmount = vectorAction[1];

        car.Move(forwardAmount * 2 - 1);
        car.Steer(steerAmount * 2 - 1);

        float positionDifference = (dummyCar.transform.position - transform.position).magnitude;
        float rotationDifference = (dummyCar.transform.eulerAngles - transform.eulerAngles).magnitude;

        // change one with a variable
        float positionReward = 1 / positionDifference;
        float rotationReward = 1 / rotationDifference;

        SetReward(rotationReward + positionReward);

        if (positionDifference > 40.0f)
        {
            SetReward(-1000);
            Done();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(transform.rotation);
        sensor.AddObservation(GetComponent<Rigidbody>().velocity);

        sensor.AddObservation(dummyCar.transform.position);
        sensor.AddObservation(dummyCar.transform.rotation);
    }

    public override void AgentReset()
    {
        transform.position = dummyCar.transform.position;
        transform.rotation = dummyCar.transform.rotation;
    }

    // Manual control
    public override float[] Heuristic()
    {
        var action = new float[2];
        car.Move(action[0]);
        car.Steer(action[1]);

        return action;
    }
}
