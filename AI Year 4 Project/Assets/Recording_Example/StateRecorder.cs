using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRecorder
{
    [SerializeField] private Rigidbody recordingObjectRig;
    private bool isRecording;
    private SerializableAgentStates serializableAgentStates;
    private SerializableImpactPoints serializableImpactPoints;
    private float fixedTimeInterval;
    private float progression;
    private Vehicle recordingVehicle;
    private long updateCounter;

    public StateRecorder(Vehicle recordingVehicle, float fixedTimeInterval)
    {
        updateCounter = 0;
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

    public void SetRecording(bool isRecording)
    {
        this.isRecording = isRecording;
    }

    public void UpdateRecorder(float deltaTime)
    {
        progression += deltaTime;

        if (progression <= fixedTimeInterval)
            return;
        progression = 0;

        if (!isRecording)
            return;

        RecordState();
    }

    private void RecordState()
    {
        // check does not allow to record animation states exceeding 10 seconds
        if (serializableAgentStates.states.Count >= 50 * 10)
            return;

        updateCounter++;

        AgentState state = new AgentState();

        state.position = recordingObjectRig.transform.localPosition;
        state.rotation = recordingObjectRig.transform.localEulerAngles;
        state.velocity = recordingObjectRig.velocity;


        if (recordingVehicle.IsColliding())
            serializableImpactPoints.impactPoitns.Add(Time.realtimeSinceStartup);    //updateCounter * fixedTimeInterval

        serializableAgentStates.states.Add(state);
        recordingVehicle.ImpactRecorded();

        /* FOR DEBUGGING ONLY - REMOVE LATER */
        /*
        Debug.Log("position: " + recordingObjectRig.gameObject.transform.position +
            "rotation: " + recordingObjectRig.gameObject.transform.rotation.eulerAngles +
            "velocity: " + recordingObjectRig.velocity);   
        */
    }

    public void StopRecorder()
    {
        isRecording = false;
    }

    // stores states and impact points in an xml file
    public void StoreStates(string directoryName, string fileName, PathBuilder.FileTypes fileType)
    {
        isRecording = false;

        string path = PathBuilder.CreateXmlFilePath(fileType,
            directoryName,
            fileName,
            false);

        XmlReadWrite.GetInstance().SaveStates(path, serializableAgentStates);
    }

    public void StoreImpactPoints(string directoryName, string fileName, PathBuilder.FileTypes fileType)
    {
        isRecording = false;

        string path = PathBuilder.CreateXmlFilePath(fileType,
            directoryName,
            fileName,
            false);

        XmlReadWrite.GetInstance().SaveImpactPoints(path, serializableImpactPoints);
    }

    public void ClearRecordedData()
    {
        serializableAgentStates.states.Clear();
    }

    /* GETTERS */
    public bool GetRecording()
    {
        return isRecording;
    }
}
