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

    private bool activeFitness;

    private bool initialized;

    public AltAgentCar() { }

    public override void InitializeAgent()
    {
        base.InitializeAgent();
    }

    public override void AgentAction(float[] vectorAction)
    {
        if (dummyCar == null)
            return;


        //Debug.Log("Aget updae");
        // increase velocities so that the agent can catch up to the dummy car
        float forwardAmount = vectorAction[0] * 1.1f;
        float steerAmount = vectorAction[1] * 1.1f;

        // apply input to the vehicle
        car.SetVehicleInput(steerAmount, forwardAmount);

        // culculate the distances in position and rotations
        float positionDifference = Vector3.Distance(dummyCar.transform.localPosition, transform.localPosition);
        float rotationDifference = Vector3.Angle(dummyCar.transform.right, transform.right);

        // give a small reward on each frame
        float distanceReward = 0;
        float rotationReward = 0;

        if (positionDifference <= 0.1)
            distanceReward = 1.0f;
        else
            distanceReward = 1 / (positionDifference * 10);

        if (rotationDifference <= 0.1)
            rotationReward = 1.0f;
        else
            rotationReward = 1 / (positionDifference * 10);

        fitness = (distanceReward * 0.5f + rotationReward * 0.5f) / 10;


        // check if the agent follows the dummy car
        //if ((positionDifference > 10 || rotationDifference > 20) && isTraining)
        if ((positionDifference > maxPosDist) && isTraining)
        {
            fitness = -10.0f;
            activeFitness = false;
            Done();
        }

        SetReward(fitness);
        // Debug.Log("Fitenss " + fitness);
        // Debug.Log("Fitness " + fitness + " distanceReward: " + distanceReward + " rotationReward: " + rotationReward);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (agentRig == null)
        {
            sensor.AddObservation(Vector3.zero);
            sensor.AddObservation(0.0f);
            sensor.AddObservation(Vector3.zero);
            sensor.AddObservation(Vector3.zero);
            sensor.AddObservation(Vector3.zero);
            sensor.AddObservation(Vector3.zero);
            sensor.AddObservation(0.0f);
            sensor.AddObservation(Vector3.zero);
            sensor.AddObservation(0.0f);
            return;
        }

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

        // We definatelly need to add this (It wasn't added for the trained model) - possibly improve training times and rapid changes in the mean fitness 
        // the mean fitness seems to be unstable
        sensor.AddObservation(dummyCarController.GetCurrentVelocity());

        sensor.AddObservation(activeFitness);
    }

    public override void AgentReset()
    {
        if (dummyCar == null || dummyCarController == null)
        {
            //dummyCar = dummyCarController.GetVehicle().GetGameObject();
            return;
        }

        // apply the dummy car state to the agent
        Vector3 posOffset = CreateRandomVector(-15, 15);
        posOffset.y = 0;
        transform.localPosition = dummyCar.transform.localPosition + posOffset;

        transform.localEulerAngles = dummyCar.transform.localEulerAngles + new Vector3(0.0f, UnityEngine.Random.Range(0, 90), 0.0f);

        Vector3 velocityOffset = CreateRandomVector(-20, 20);
        velocityOffset.y = 0;
        agentRig.velocity = dummyCarController.GetCurrentVelocity() + velocityOffset;
    }

    public Vector3 CreateRandomVector(float minMagnitude, float maxMagnitude)
    {
        float magnitude = UnityEngine.Random.Range(minMagnitude, maxMagnitude);

        Vector3 output = new Vector3(UnityEngine.Random.Range(-0.0f, 2.0f),
            0,
            UnityEngine.Random.Range(0.0f, 2.0f));

        output = output.normalized * magnitude;

        return output;
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

