using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CarVehicle))]
public class AgentCar : AbstractAgentCarController
{
    // A reference to the vehicle "driven" by the agent
    private AbstractVehicle car;

    // a reference to the dummy car
    private AbstractDummyCarController dummyCarController;

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

    public AgentCar() { }

    public override void InitializeAgent()
    {
        base.InitializeAgent();
    }

    public override void AgentAction(float[] vectorAction)
    {
        if(car == null || dummyCar == null)
            return;
            
        // increase velocities so that the agent can catch up to the dummy car
        float forwardAmount = vectorAction[0] * 1.1f;
        float steerAmount = vectorAction[1] * 1.1f;

        // apply input to the vehicle
        car.SetVehicleInput(steerAmount, forwardAmount);

        // culculate the distances in position and rotations
        float positionDifference = (dummyCar.transform.localPosition - transform.localPosition).magnitude;
        float rotationDifference = Vector3.Angle(dummyCar.transform.right, transform.right);

        // give a small reward on each frame
        fitness = 0.01f;

        if ((positionDifference > maxPosDist || rotationDifference > maxRotDist) && isTraining)
        {
            fitness = -1.0f;
            Done();
        }

        //Debug.Log("Reward: " + reward + " Pos diff: " + positionDifference + " Rot diff: " + rotationDifference);
        SetReward(Mathf.Pow(fitness, 1));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (agentRig == null)
            return;

        // pass agent information
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(transform.localEulerAngles.y);
        sensor.AddObservation(agentRig.velocity);

        // pass distance between agent and dummy car
        sensor.AddObservation(dummyCar.transform.localPosition - transform.localPosition);

        // pass rotation difference between agent and dummy car
        sensor.AddObservation(Vector3.Angle(dummyCar.transform.right, transform.right));

        // pass dummy car information
        sensor.AddObservation(dummyCar.transform.localPosition);
        sensor.AddObservation(dummyCar.transform.localEulerAngles.y);

        // padd the dummy car's velocity
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

    public override void InitController(AbstractVehicle vehicle)
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
        environment.GetDummyCarController().ResetOccuredEvent += AgentReset;
    }

    public override void ResetVehicleController()
    {
        throw new NotImplementedException();
    }
}