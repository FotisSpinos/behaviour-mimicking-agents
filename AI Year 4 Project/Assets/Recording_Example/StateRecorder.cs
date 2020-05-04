using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRecorder
{
    [SerializeField] private Rigidbody recordingObjectRig;
    private bool isRecording;
    private SerializableAgentStates serializableAgentStates;
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
        serializableAgentStates = new SerializableAgentStates();
        this.fixedTimeInterval = fixedTimeInterval;
        serializableAgentStates.captureRate = fixedTimeInterval;
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

        //StatesManager.GetInstance().AddState(0, state);
        //xml.AddState(state);

        if (recordingVehicle.IsColliding())
            serializableAgentStates.impactPoitns.Add(updateCounter * fixedTimeInterval);

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

    public void StoreRecordedData(string directoryName, string fileName)
    {
        isRecording = false;
        XmlReadWrite.GetInstance().SaveStates(directoryName, fileName, serializableAgentStates);
    }

    public void ClearRecordedData()
    {
        //StatesManager.GetInstance().ClearData(0);
        serializableAgentStates.states.Clear();
    }

    /* GETTERS */
    public bool GetRecording()
    {
        return isRecording;
    }
}
