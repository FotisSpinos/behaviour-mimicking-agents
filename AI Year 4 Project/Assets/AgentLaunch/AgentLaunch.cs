using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

[RequireComponent(typeof(Rigidbody))]
public class AgentLaunch : Agent
{
    [SerializeField] private TrainingEnvironmnetMaster environment;

    // A reference to the vehicle "driven" by the agent
    private Vehicle car;

    [SerializeField] private GameObject dummyCar;

    private TrainingVehicleController dummyCarController;

    private Rigidbody agentRig;

    public override void InitializeAgent()
    {
        base.InitializeAgent();

        car = gameObject.GetComponent<CarVehicle>();

        agentRig = GetComponent<Rigidbody>();

        // initialize environment master
        environment.InitEnvironmentMaster();

        dummyCarController = environment.GetDummyCarController();

        environment.GetDummyCarController().OnReset += AgentReset;
    }

    public override void AgentAction(float[] vectorAction)
    {
        // apply actions
        float forwardAmount = vectorAction[0] * 1.4f;
        float steerAmount = vectorAction[1] * 1.5f;

        car.SetVehicleInput(steerAmount, forwardAmount);

        float positionDifference = (dummyCar.transform.localPosition - transform.localPosition).magnitude * 1000;
        float rotationDifference = (dummyCar.transform.localEulerAngles - transform.localEulerAngles).magnitude * 1000;

        if (positionDifference > 10000 || rotationDifference > 20000)
        {
            Done();
        }

        // update the environemnt
        environment.UpdateEnvironmentMaster();
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
}
