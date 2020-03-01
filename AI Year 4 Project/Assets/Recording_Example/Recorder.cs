using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder
{
    [SerializeField] private Rigidbody recordingObject;
    private LinkedList<Vector3> positions;
    private LinkedList<Vector3> rotations;
    private LinkedList<Vector3> velocities;
    private bool isRecording;

    public Recorder(Rigidbody recordingObject)
    {
        positions = new LinkedList<Vector3>();
        rotations = new LinkedList<Vector3>();
        velocities = new LinkedList<Vector3>();
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
        positions.AddLast(recordingObject.gameObject.transform.position);
        rotations.AddLast(recordingObject.gameObject.transform.rotation.eulerAngles);
        velocities.AddLast(recordingObject.velocity);

        /* FOR DEBUGGING ONLY - REMOVE LATER */
        /*
        Debug.Log("position: " + recordingObject.gameObject.transform.position +
            "rotation: " + recordingObject.gameObject.transform.rotation.eulerAngles +
            "velocity: " + recordingObject.velocity);
        */
    }

    /* GETTERS */

    public LinkedList<Vector3> GetPositions()
    {
        return positions;
    }

    public LinkedList<Vector3> GetRotations()
    {
        return rotations;
    }

    public LinkedList<Vector3> GetVelocities()
    {
        return velocities;
    }

    public bool GetRecording()
    {
        return isRecording;
    }
}
