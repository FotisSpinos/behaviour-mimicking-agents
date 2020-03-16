using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CarVehicle))]
public class AgentCar : Agent
{
    // A reference to the vehicle "driven" by the agent
    private Vehicle car;

    // a reference to the dummy car
    private DummyCarController dummyCarController;

    /*
     * It was noted that the update and AgentAction were not synchronized
     * The agent action was running 3 - 4 times before an update excecutes
     * Because of that the agent was mimicking a "Slow" dummy car
     * Thus environment calls ensured that the dummy car and the agent are updating at the same rate
    */
    [SerializeField] private BaseEnvironmentMaster environment;

    // the agent's rigidbody
    private Rigidbody agentRig;

    // the dummy car game object
    private GameObject dummyCar;

    // the maximum rotation distance between the dummy car and the agent
    [SerializeField] float maxRotDist;

    // the maximum position distance between the dummy car and the agent
    [SerializeField] float maxPosDist;

    public override void InitializeAgent()
    {
        base.InitializeAgent();

        // initialize environment master
        environment.InitEnvironmentMaster();

        // update once
        environment.UpdateEnvironmentMaster();

        // add car vehicle to this game object
        car = gameObject.GetComponent<CarVehicle>();

        // Set class attributes
        dummyCarController = environment.GetDummyCarController();
        dummyCar = dummyCarController.GetVehicle().GetGameObject();

        transform.localPosition = dummyCar.transform.position;
        transform.localEulerAngles = dummyCar.transform.localEulerAngles;

        agentRig = GetComponent<Rigidbody>();
        
        // subscribe to the animation reset event
        environment.GetDummyCarController().OnReset += AgentReset;

        // check that parameters set from the editor are valid
        
    }

    public override void AgentAction(float[] vectorAction)
    {
        // reset reward value at every update
        float reward = 0;

        // increase velocities so that the agent can catch up to the dummy car
        float forwardAmount = vectorAction[0] * 1.4f;
        float steerAmount = vectorAction[1] * 1.5f;

        // apply input to the vehicle
        car.SetVehicleInput(steerAmount, forwardAmount);

        // culculate the distances in position and rotations
        float positionDifference = (dummyCar.transform.localPosition - transform.localPosition).magnitude;
        float rotationDifference = (dummyCar.transform.localEulerAngles - transform.localEulerAngles).magnitude;

        // give a small reward on each frame
        reward = 0.01f;

        // check if the agent follows the dummy car
        if (positionDifference > 10 || rotationDifference > 20)
        {
            reward = -1.0f;
           // Done();
        }

        // update the environemnt
        environment.UpdateEnvironmentMaster();

        //Debug.Log("Reward: " + reward + " Pos diff: " + positionDifference + " Rot diff: " + rotationDifference);
        SetReward(Mathf.Pow(reward, 1));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // pass agent information
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(transform.localEulerAngles.z);
        sensor.AddObservation(agentRig.velocity);

        // pass distance between agent and dummy car
        sensor.AddObservation(dummyCar.transform.position - transform.position);

        // pass dummy car information
        sensor.AddObservation(dummyCar.transform.localPosition);
        sensor.AddObservation(dummyCar.transform.localEulerAngles.z);

        // We definatelly need to add this (It wasn't added for the trained model) - possibly improve training times and rapid changes in the mean fitness 
        // the mean fitness seems to be unstable
        Vector3 dummyCarVelocity = dummyCarController.GetCurrentVelocity();
        sensor.AddObservation(dummyCarVelocity);
    }

    public override void AgentReset()
    {
        // reset the animation randomly
        if (environment != null && environment.GetDummyCarController() != null)
        {
            environment.GetDummyCarController().ResetVehicleController();
        }

        // apply the dummy car state to the agent
        transform.localPosition = dummyCar.transform.localPosition;
        transform.localRotation = dummyCar.transform.localRotation;
        agentRig.velocity = dummyCarController.GetCurrentVelocity();
    }

    // Manual control
    public override float[] Heuristic()
    {
        float[] action = new float[2];
        action[0] = Input.GetAxis("Vertical");
        action[1] = Input.GetAxis("Horizontal");

        return action;
    }
}
