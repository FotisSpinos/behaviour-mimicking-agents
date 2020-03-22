using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder
{
    [SerializeField] private Rigidbody recordingObject;
    private XML_Manager xml = GameObject.Find("Obj_GameManager").GetComponent<XML_Manager>();
    private bool isRecording;

    public Recorder(Rigidbody recordingObject)
    {
        isRecording = false;
        this.recordingObject = recordingObject;

        //xml = new XML_Manager();
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

        state.position = recordingObject.transform.localPosition;
        state.rotation = recordingObject.transform.localEulerAngles;
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
        xml.Save(Files.CarPath_2);
    }

    /* GETTERS */
    public bool GetRecording()
    {
        return isRecording;
    }
}
