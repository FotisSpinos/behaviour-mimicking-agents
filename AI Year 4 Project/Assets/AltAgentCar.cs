using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CarVehicle))]
public class AltAgentCar : AgentCarController
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

    [SerializeField] bool isTraining = true;

    public AltAgentCar() { }

    public override void InitializeAgent()
    {
        base.InitializeAgent();
    }

    public override void AgentAction(float[] vectorAction)
    {
        Debug.Log("The agent updates");

        // reset reward value at every update
        float reward = 0;

        // increase velocities so that the agent can catch up to the dummy car
        float forwardAmount = vectorAction[0] * 1.4f;
        float steerAmount = vectorAction[1] * 1.5f;

        // apply input to the vehicle
        car.SetVehicleInput(steerAmount, forwardAmount);

        // culculate the distances in position and rotations
        float positionDifference = Vector3.Distance(dummyCar.transform.localPosition, transform.localPosition);
        float rotationDifference = Vector3.Angle(dummyCar.transform.right, transform.right);

        // give a small reward on each frame
        float distanceReward = 0;
        float rotationReward = 0;

        distanceReward = 1 / positionDifference / 100;
        rotationReward = 1 / rotationDifference;

        if (positionDifference == 0)
            distanceReward = 0.1f;
        if (rotationDifference == 0)
            rotationReward = 0.1f;

        reward = positionDifference + rotationDifference;


        // check if the agent follows the dummy car
        //if ((positionDifference > 10 || rotationDifference > 20) && isTraining)
        if ((positionDifference > maxPosDist || rotationDifference > maxRotDist) && isTraining)
        {
            reward = -1000.0f;
            Done();
        }

        //Debug.Log("Reward: " + reward + " Pos diff: " + positionDifference + " Rot diff: " + rotationDifference);
        SetReward(Mathf.Pow(reward, 1));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (sensor == null || agentRig == null)
            return;

        // pass agent information
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(transform.localEulerAngles.y);
        sensor.AddObservation(agentRig.velocity);

        // pass distance between agent and dummy car
        sensor.AddObservation(dummyCar.transform.localPosition - transform.localPosition);

        // pass rotation difference between agent and dummy car
        sensor.AddObservation(dummyCar.transform.localEulerAngles - transform.localEulerAngles);

        // pass dummy car information
        sensor.AddObservation(dummyCar.transform.localPosition);
        sensor.AddObservation(dummyCar.transform.localEulerAngles.y);

        // We definatelly need to add this (It wasn't added for the trained model) - possibly improve training times and rapid changes in the mean fitness 
        // the mean fitness seems to be unstable
        Vector3 dummyCarVelocity = dummyCarController.GetCurrentVelocity();
        sensor.AddObservation(dummyCarVelocity);
    }

    public override void AgentReset()
    {
        if (dummyCar == null)
            return;
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

    public override void UpdateController()
    {
        throw new NotImplementedException();
    }

    public override void InitController(Vehicle vehicle)
    {
        // add car vehicle to this game object
        car = vehicle; 

        // Set class attributes
        dummyCarController = environment.GetDummyCarController();
        dummyCar = dummyCarController.GetVehicle().GetGameObject();

        transform.localPosition = dummyCar.transform.position;
        transform.localEulerAngles = dummyCar.transform.localEulerAngles;

        agentRig = GetComponent<Rigidbody>();

        // subscribe to the animation reset event
        environment.GetDummyCarController().OnReset += AgentReset;
    }

    public override void ResetVehicleController()
    {
        throw new NotImplementedException();
    }
}
