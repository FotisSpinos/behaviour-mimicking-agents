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

    [SerializeField] private float resetTime = 10.0f;
    private float timer;

    [SerializeField] private TrainingEnvironmnetMaster environment;

    private Vector3 spawnPos;

    private void Start()
    {
        // add car vehicle to this game object
        car = gameObject.GetComponent<CarVehicle>();

        transform.localPosition = dummyCar.transform.localPosition;//dummyCar.transform.localPosition;
        transform.localRotation = dummyCar.transform.localRotation;
    }

    public override void AgentAction(float[] vectorAction)
    {
        float reward = 0;

        // apply actions
        float forwardAmount = vectorAction[0] * 1.4f;
        float steerAmount = vectorAction[1] * 1.5f;

        car.SetVehicleInput(steerAmount, forwardAmount);

        float positionDifference = (dummyCar.transform.localPosition - transform.localPosition).magnitude * 1000;
        float rotationDifference = (dummyCar.transform.localEulerAngles - transform.localEulerAngles).magnitude * 1000;

        if (positionDifference <= 0.1f)
            positionDifference = 1f;
        if (rotationDifference <= 0.1f)
            rotationDifference = 1f;

        float positionReward = 1 / positionDifference;
        float rotationReward = 1 / rotationDifference;

        //reward = (positionReward * 1.5f + rotationReward * 0.5f) / 2;
        reward = 0.1f;

        //if(positionDifference > 10000)
        {
            reward = -1.0f;
            //Done();
        }

        /*
        if (timer > resetTime)
        {
            timer = 0.0f;
            Done();
        }
        */
        timer += Time.deltaTime;

        //Debug.Log("Reward: " + reward + " Pos diff: " + positionDifference + " Rot diff: " + rotationDifference);
        SetReward(Mathf.Pow(reward, 1));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(transform.localEulerAngles);
        sensor.AddObservation(GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(dummyCar.transform.position - transform.position);

        sensor.AddObservation(dummyCar.transform.localPosition);
        sensor.AddObservation(dummyCar.transform.localEulerAngles);
    }

    public override void AgentReset()
    {
        if (environment != null && environment.GetDummyCarController() != null)
            environment.GetDummyCarController().ResetVehicleController();

        if (spawnPos == Vector3.zero)
            spawnPos = transform.localPosition;

        transform.localPosition = dummyCar.transform.localPosition;//dummyCar.transform.localPosition;
        transform.localRotation = dummyCar.transform.localRotation;
    }

    // Manual control
    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");

        return action;
    }
}
