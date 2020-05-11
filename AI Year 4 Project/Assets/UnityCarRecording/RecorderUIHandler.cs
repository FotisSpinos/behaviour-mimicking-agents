using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecorderUIHandler : MonoBehaviour
{
    [SerializeField] protected InputField directoryInputField;
    [SerializeField] protected InputField fileNameInputField;
    [SerializeField] protected Button recordButton;
    [SerializeField] protected Text recordButtonText;
    [SerializeField] protected Rigidbody targetRig;

    private AbstractRecorder stateRecorder;

    public AbstractRecorder StateRecorder
    {
        set
        {
            stateRecorder = value;
        }
    }

    public void Start()
    {
        recordButton.onClick.AddListener(OnStartRecordingPressed);
        recordButtonText.text = "Start Recording";
    }

    protected void OnStartRecordingPressed()
    {
        if(stateRecorder == null)
        {
            Debug.LogWarning("Recorder has not been assign in RecorderUIHandler");
            return;
        }

        stateRecorder.SetRecording(!stateRecorder.GetRecording());
        
        Debug.Log("Recorder Started");

        recordButtonText.text = "Stop Recording";

        // save data if we stop recording
        if (!stateRecorder.GetRecording())
        {
            recordButtonText.text = "Start Recording";

            stateRecorder.StoreStates(GetInputFieldText(fileNameInputField), GetInputFieldText(directoryInputField), PathBuilder.FileTypes.ANIMATION_DATA);
            stateRecorder.ClearRecordedData();
            Debug.Log("Recorder Stoped");
        }
    }

    private string GetInputFieldText(InputField inputField)
    {
        if (inputField == null || inputField.text == "")
            return "Default";
        return inputField.text;
    }

    public RecorderUIHandler()
    {    }
}
