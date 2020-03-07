using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder
{
    [SerializeField] private Rigidbody recordingObject;
    private XML_Manager xml;
    private bool isRecording;

    public Recorder(Rigidbody recordingObject)
    {
        isRecording = false;
        this.recordingObject = recordingObject;

        xml = new XML_Manager();
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
        Agent_State state = new Agent_State();

        state.position = recordingObject.transform.position;
        state.rotation = recordingObject.transform.rotation.eulerAngles;
        state.velocity = recordingObject.velocity;

        xml.AddState(state);

        /* FOR DEBUGGING ONLY - REMOVE LATER */
        
        Debug.Log("position: " + recordingObject.gameObject.transform.position +
            "rotation: " + recordingObject.gameObject.transform.rotation.eulerAngles +
            "velocity: " + recordingObject.velocity);
        
    }

    // stops recording and stores gathered data to xml file
    public void StoreRecordedData()
    {
        isRecording = false;
        xml.Save(Files.DummyCar);
    }

    /* GETTERS */
    public bool GetRecording()
    {
        return isRecording;
    }
}
