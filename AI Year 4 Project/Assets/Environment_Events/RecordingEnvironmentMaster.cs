/*
 * Contains Scene information
 * Allows to access objects significant for testing and training scenes
 * Only one Game Object with this component should exist in a scene
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordingEnvironmentMaster : BaseEnvironmentMaster
{
    // the instance of the environment master
    private static EnvironmentMaster instance;

    // a reference to the recorder
    protected StateRecorder recorder;

    // the vehicle controller operated by the user
    private VehicleController userVehicleController;

    [SerializeField] protected InputField directoryInputField;
    [SerializeField] protected InputField fileNameInputField;
    [SerializeField] protected Button recordButton;
    [SerializeField] protected Text recordButtonText;
    [SerializeField] protected Rigidbody targetRig;

    // default constructor
    public RecordingEnvironmentMaster() {}

    public static EnvironmentMaster GetInstance()
    {
        if(instance == null)
        {
            GameObject envMasterGo = new GameObject();
            envMasterGo.AddComponent<RecordingEnvironmentMaster>();
        }
        return instance;
    }

    public override DummyCarController GetDummyCarController()
    {
        return null;
    }

    override public void InitEnvironmentMaster()
    {
        // make a recorder instance
        recorder = new StateRecorder(targetRig.GetComponent<CarVehicle>(), 0.02f);

        userVehicleController = new UserVehicleController(targetRig.GetComponent<CarVehicle>());
        userVehicleController.InitController(targetRig.GetComponent<CarVehicle>());

        recordButton.onClick.AddListener(OnStartRecordingPressed);
        recordButtonText.text = "Start Recording";
    }

    override public void UpdateEnvironmentMaster()
    {   
        // record the state
        recorder.UpdateRecorder(Time.deltaTime);

        // update vehicle controller
        userVehicleController.UpdateController();
    }

    protected void OnStartRecordingPressed()
    {
        recorder.SetRecording(!recorder.GetRecording());
        Debug.Log("Recorder Started");

        recordButtonText.text = "Stop Recording";

        if(recorder.GetRecording())
        {
            LoadedStates.GetInstance().AddSerializableAgentState(new SerializableAgentStates());
        }

        // save data if we stop recording
        if (!recorder.GetRecording())
        {
            recordButtonText.text = "Start Recording";

            recorder.StoreStates(GetInputFieldText(fileNameInputField), GetInputFieldText(directoryInputField), PathBuilder.FileTypes.ANIMATION_DATA);
            recorder.ClearRecordedData();
            Debug.Log("Recorder Stoped");
        }
    }

    private string GetInputFieldText(InputField inputField)
    {
        if (inputField == null || inputField.text == "")
            return "Default";
        return inputField.text;
    }
}
