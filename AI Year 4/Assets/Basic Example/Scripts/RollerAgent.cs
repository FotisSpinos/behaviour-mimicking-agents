using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MLAgents;
using System;

public class RollerAgent : Agent
{
    private Rigidbody rBody;
    private Transform target;
    private float speed = 10.0f;

    [SerializeField]
    private Material targetMat;

    void Start() 
    {
        rBody = GetComponent<Rigidbody>();

        //Create and spawn target object
        GameObject targetObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        targetObj.name = "Target Object";
        target = targetObj.transform;
        target.parent = transform.parent;

        if(targetMat != null)
            targetObj.GetComponent<MeshRenderer>().material = targetMat;

        SpawnTarget();
    }

    // Set target's location randomly
    private void SpawnTarget()
    {
        target.position = target.parent.position + 
            new Vector3(UnityEngine.Random.value * 8 - 4,
            0.5f,
            UnityEngine.Random.value * 8 - 4);
    }

    // Resets the environment and agents
    public override void AgentReset() 
    {
        base.AgentReset();

        if(this.transform.position.y < 0 ) {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0, 0.5f, 0.5f);
        }

        SpawnTarget();
    }

    // Collects all observations and feeds them to the agent
    public override void CollectObservations(VectorSensor sensor) 
    {
        base.CollectObservations(sensor);

        // Target and Agent positions
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    // Reseives inputs and translates them into actions 
    public override void AgentAction(float[] vectorAction)
    {
        base.AgentAction(vectorAction);

        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rBody.AddForce(controlSignal * speed);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.position,
                                                  target.position);

        // Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            Done();
        }

        // Fell off platform
        if (this.transform.position.y < 0)
        {
            Done();
        }
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
