using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRecorder : AbstractRecorder
{
    [SerializeField] private Rigidbody recordingObjectRig;

    private float fixedTimeInterval;
    private float progression;
    private Vehicle recordingVehicle;

    // Storing data
    private SerializableAgentStates serializableAgentStates;
    private SerializableImpactPoints serializableImpactPoints;

    public StateRecorder(Vehicle recordingVehicle, float fixedTimeInterval)
    {
        isRecording = false;

        this.recordingVehicle = recordingVehicle;
        this.recordingObjectRig = recordingVehicle.GetGameObject().GetComponent<Rigidbody>();
        
        // init serializable agent states
        serializableAgentStates = new SerializableAgentStates();
        serializableAgentStates.captureRate = fixedTimeInterval;

        // init serializable impact points
        serializableImpactPoints = new SerializableImpactPoints();
        serializableImpactPoints.captureRate = fixedTimeInterval;

        this.fixedTimeInterval = fixedTimeInterval;
    }

    protected override void RecordState()
    {
        // check does not allow to record animation states exceeding 10 seconds
        if (serializableAgentStates.states.Count >= 50 * 60)
            return;

        AgentState state = new AgentState();

        state.position = recordingObjectRig.transform.localPosition;
        state.rotation = recordingObjectRig.transform.localEulerAngles;
        state.velocity = recordingObjectRig.velocity;


        if (recordingVehicle.IsColliding())
            serializableImpactPoints.impactPoitns.Add(Time.realtimeSinceStartup);

        serializableAgentStates.states.Add(state);
        recordingVehicle.ImpactRecorded();

        /* FOR DEBUGGING ONLY - REMOVE LATER */
        /*
        Debug.Log("position: " + recordingObjectRig.gameObject.transform.position +
            "rotation: " + recordingObjectRig.gameObject.transform.rotation.eulerAngles +
            "velocity: " + recordingObjectRig.velocity);   
        */
    }

    // stores states and impact points in an xml file
    public override void StoreStates(string directoryName, string fileName, PathBuilder.FileTypes fileType)
    {
        isRecording = false;

        string path = PathBuilder.CreateXmlFilePath(fileType,
            directoryName,
            fileName,
            false);

        XmlReadWrite.GetInstance().SaveStates(path, serializableAgentStates);
    }

    public override void StoreImpactPoints(string directoryName, string fileName, PathBuilder.FileTypes fileType)
    {
        isRecording = false;

        string path = PathBuilder.CreateXmlFilePath(fileType,
            directoryName,
            fileName,
            false);

        XmlReadWrite.GetInstance().SaveImpactPoints(path, serializableImpactPoints);
    }

    public override void ClearRecordedData()
    {
        serializableAgentStates.states.Clear();
    }
}
