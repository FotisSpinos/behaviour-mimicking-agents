using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MLAgents;
using System;

// we need a decision requester component
// we need behaviour parameters component

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CarVehicle))]
public class AgentCar : Agent
{
    // A reference to the vehicle "driven" by the agent
    private Vehicle car;

    // a reference to the dummy car
    private TrainingVehicleController dummyCarController;

    /*
     * It was noted that the update and AgentAction were not synchronized
     * The agent action was running 3 - 4 times before an update excecutes
     * Because of that the agent was mimicking a "Slow" dummy car
     * Thus environment calls ensured that the dummy car and the agent are updating at the same rate
    */
    [SerializeField] private TrainingEnvironmnetMaster environment;

    // the agent's rigidbody
    private Rigidbody agentRig;

    private GameObject dummyCar;

    public override void InitializeAgent()
    {
        base.InitializeAgent();

        // initialize environment master
        environment.InitEnvironmentMaster();

        // add car vehicle to this game object
        car = gameObject.GetComponent<CarVehicle>();

        dummyCarController = environment.GetDummyCarController();

        dummyCar = dummyCarController.GetVehicle().GetGameObject();

        transform.localPosition = dummyCar.transform.position;
        transform.localEulerAngles = dummyCar.transform.localEulerAngles;

        agentRig = GetComponent<Rigidbody>();
        
        environment.GetDummyCarController().OnReset += AgentReset;
    }

    public override void AgentAction(float[] vectorAction)
    {
        // reset reward value at every update
        float reward = 0;

        // apply actions
        float forwardAmount = vectorAction[0] * 1.4f;
        float steerAmount = vectorAction[1] * 1.5f;

        car.SetVehicleInput(steerAmount, forwardAmount);

        float positionDifference = (dummyCar.transform.localPosition - transform.localPosition).magnitude * 1000;
        float rotationDifference = (dummyCar.transform.localEulerAngles - transform.localEulerAngles).magnitude * 1000;

        //if (positionDifference <= 0.1f)
        //    positionDifference = 1f;
        //if (rotationDifference <= 0.1f)
        //    rotationDifference = 1f;

        //float positionReward = 1 / positionDifference;
        //float rotationReward = 1 / rotationDifference;

        //reward = (positionReward * 1.5f + rotationReward * 0.5f) / 2;
        reward = 0.01f;

        if(positionDifference > 10000 || rotationDifference > 20000)
        {
            reward = -1.0f;
            Done();
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
        sensor.AddObservation(transform.localEulerAngles.y);
        sensor.AddObservation(agentRig.velocity);

        // pass distance between agent and dummy car
        sensor.AddObservation(dummyCar.transform.position - transform.position);

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
        if (environment != null && environment.GetDummyCarController() != null)
            environment.GetDummyCarController().ResetVehicleController();

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

/*
 WORKING OBSERVATIONS: 


        // pass agent information
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(transform.localEulerAngles);
        sensor.AddObservation(agentRig);

        // pass distance between agent and dummy car
        sensor.AddObservation(dummyCar.transform.position - transform.position);

        // pass dummy car information
        sensor.AddObservation(dummyCar.transform.localPosition);
        sensor.AddObservation(dummyCar.transform.localEulerAngles);
 */
