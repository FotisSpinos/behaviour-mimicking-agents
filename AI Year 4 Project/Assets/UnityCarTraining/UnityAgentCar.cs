using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

public class UnityAgentCar : AbstractAgentCarController
{
    // A reference to the vehicle "driven" by the agent
    private Vehicle agentVehicle;

    // a reference to the dummy car
    private UnityDummyCar unityDummyCarController;

    // a reference to the environment
    [SerializeField] private UnityAgentEnvrionmentMaster environment;

    // the agent's rigidbody
    private Rigidbody agentRig;

    // the dummy car game object
    private GameObject dummyCar;

    // the maximum rotation distance between the dummy car and the agent
    [SerializeField] private float maxRotDist;

    // the maximum position distance between the dummy car and the agent
    [SerializeField] private float maxPosDist;

    [SerializeField] private bool canReset = true;

    [SerializeField] CarController agentCarController;
    private CarController.Atributes carControllerAtr;

    public override void InitController(Vehicle vehicle)
    {
        // Set class attributes
        this.agentVehicle = GetComponent<UnityVehicle>();

        unityDummyCarController = environment.GetUnityDummyCar();
        dummyCar = unityDummyCarController.GetVehicle().GetGameObject();

        transform.localPosition = dummyCar.transform.position;
        transform.localEulerAngles = dummyCar.transform.localEulerAngles;

        agentRig = GetComponent<Rigidbody>();

        carControllerAtr = agentCarController.GetCarControllerAtr();

        // subscribe to the animation reset event
        environment.GetUnityDummyCar().OnReset += AgentReset;
    }

    public override void ResetVehicleController(){}
    public override void UpdateController() {}

    public override void InitializeAgent()
    {
        base.InitializeAgent();
    }

    public override void AgentReset()
    {
        if (dummyCar == null)
            return;

        // apply the dummy car state to the agent
        agentCarController.SetCarControllerAtr(unityDummyCarController.GetCurrentAtribute());
    }

    public override float[] Heuristic()
    {
        float[] action = new float[3];
        action[0] = Input.GetAxisRaw("Horizontal");
        action[1] = Input.GetAxisRaw("Vertical");
        action[2] = CrossPlatformInputManager.GetAxis("Jump");

        return action;
    }

    public override void AgentAction(float[] vectorAction)
    {
        agentVehicle.SetVehicleInput(vectorAction); 

        // measure the distance in position and rotation
        float positionDifference = (dummyCar.transform.localPosition - transform.localPosition).magnitude;
        float rotationDifference = Vector3.Angle(dummyCar.transform.right, transform.right);

        // give a small reward on each frame
        fitness = 0.01f;

        // check if we need to reset the environment
        if ((positionDifference > maxPosDist || rotationDifference > maxRotDist) && canReset)
        {
            fitness = -1.0f;
            Done();
        }

        //Debug.Log("Reward: " + reward + " Pos diff: " + positionDifference + " Rot diff: " + rotationDifference);
        SetReward(Mathf.Pow(fitness, 1));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if(agentRig == null)
        {
            this.InitController(null);
        }

        carControllerAtr = agentCarController.GetCarControllerAtr();
        sensor.AddObservation(Normalize.NormVec(carControllerAtr.localPosition, new Vector3(250.0f, 50.0f, 250.0f)));
        sensor.AddObservation(Normalize.NormVec(carControllerAtr.localEulerAngles, new Vector3(0.0f, 0.0f ,0.0f), new Vector3(360.0f, 360.0f, 360.0f)));
        sensor.AddObservation(Normalize.NormVec(carControllerAtr.velocity, new Vector3(80.0f, 80.0f, 80.0f)));
        sensor.AddObservation(Normalize.NormNum(carControllerAtr.m_CurrentTorque, 0, 2500));
        sensor.AddObservation(carControllerAtr.m_GearFactor);
        sensor.AddObservation(Normalize.NormNum(carControllerAtr.m_GearNum, 0.0f, 4.0f));
        sensor.AddObservation(Normalize.NormNum(carControllerAtr.m_OldRotation, -0.1f, 360.0f));
        sensor.AddObservation(Normalize.NormVec(agentRig.angularVelocity, new Vector3(0.5f, 2.0f, 0.5f)));
        
        for (int i = 0; i < carControllerAtr.m_WheelMeshLocalRotations.Length; i++)
        {
            sensor.AddObservation(Normalize.NormQuat(carControllerAtr.m_WheelMeshLocalRotations[i]));
        }


        sensor.AddObservation(Normalize.NormVec(unityDummyCarController.GetCurrentAtribute().localPosition, new Vector3(250.0f, 50.0f, 250.0f)));
        sensor.AddObservation(Normalize.NormVec(unityDummyCarController.GetCurrentAtribute().localEulerAngles, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(360.0f, 360.0f, 360.0f)));
        sensor.AddObservation(Normalize.NormVec(unityDummyCarController.GetCurrentAtribute().velocity, new Vector3(80.0f, 80.0f, 80.0f)));
        sensor.AddObservation(Normalize.NormNum(unityDummyCarController.GetCurrentAtribute().m_CurrentTorque, 0, 2500));
        sensor.AddObservation(unityDummyCarController.GetCurrentAtribute().m_GearFactor);
        sensor.AddObservation(Normalize.NormNum(unityDummyCarController.GetCurrentAtribute().m_GearNum, 0.0f, 4.0f));
        sensor.AddObservation(Normalize.NormNum(unityDummyCarController.GetCurrentAtribute().m_OldRotation, -0.1f, 360.0f));
        sensor.AddObservation(Normalize.NormVec(unityDummyCarController.GetCurrentAtribute().angularVelocity, new Vector3(0.5f, 2.0f, 0.5f)));

        for (int i = 0; i < unityDummyCarController.GetCurrentAtribute().m_WheelMeshLocalRotations.Length; i++)
        {
            sensor.AddObservation(Normalize.NormQuat(unityDummyCarController.GetCurrentAtribute().m_WheelMeshLocalRotations[i]));
        }

        sensor.AddObservation((unityDummyCarController.GetCurrentAtribute().localPosition - carControllerAtr.localPosition).magnitude / (maxPosDist + 0.5f));

        float angle = Vector3.Angle(unityDummyCarController.GetVehicle().GetGameObject().transform.forward, gameObject.transform.forward);

        sensor.AddObservation(angle / (maxRotDist + 5));
    }
}