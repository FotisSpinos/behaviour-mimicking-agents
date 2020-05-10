using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractRecorder
{
    protected bool isRecording;
    protected float progression;
    protected float fixedTimeInterval;

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

    protected abstract void RecordState();

    public abstract void ClearRecordedData();

    public abstract void StoreStates(string directoryName, string fileName, PathBuilder.FileTypes fileType);

    public abstract void StoreImpactPoints(string directoryName, string fileName, PathBuilder.FileTypes fileType);

    public void SetRecording(bool isRecording)
    {
        this.isRecording = isRecording;
    }

    public void StopRecorder()
    {
        isRecording = false;
    }

    public bool GetRecording()
    {
        return isRecording;
    }
}
