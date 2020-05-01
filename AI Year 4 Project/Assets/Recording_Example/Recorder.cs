using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder
{
    [SerializeField] private Rigidbody recordingObject;
    private bool isRecording;

    public Recorder(Rigidbody recordingObject)
    {
        isRecording = false;
        this.recordingObject = recordingObject;
    }

    public void SetRecording(bool isRecording)
    {
        this.isRecording = isRecording;
    }

    public void UpdateRecorder()
    {
        if (!isRecording)
            return;

        RecordState();
    }

    private void RecordState()
    {
        AgentState state = new AgentState();

        state.position = recordingObject.transform.localPosition;
        state.rotation = recordingObject.transform.localEulerAngles;
        state.velocity = recordingObject.velocity;

        StatesManager.GetInstance().AddState(0 ,state);
        //xml.AddState(state);

        /* FOR DEBUGGING ONLY - REMOVE LATER */
        Debug.Log("position: " + recordingObject.gameObject.transform.position +
            "rotation: " + recordingObject.gameObject.transform.rotation.eulerAngles +
            "velocity: " + recordingObject.velocity);
        
    }

    public void StopRecorder()
    {
        isRecording = false;
    }

    public void StoreRecordedData(string directoryName, string fileName)
    {
        isRecording = false;
        XmlReadWrite.GetInstance().SaveStates(directoryName, fileName, StatesManager.GetInstance().GetSerializableAgentState(0));
    }

    public void ClearRecordedData()
    {
        StatesManager.GetInstance().ClearData(0);
    }

    /* GETTERS */
    public bool GetRecording()
    {
        return isRecording;
    }
}
