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
    
    [SerializeField] private Rigidbody targetRig;

    // check if we are recording
    private bool isRecording;

    // a reference to the recorder
    private Recorder recorder;

    // the vehicle controller operated by the user
    private VehicleController userVehicleController;

    // the file index storing recorded current animation 
    int animIndex;

    [SerializeField] private InputField directoryInputField;
    [SerializeField] private InputField fileNameInputField;
    [SerializeField] private Button recordButton;
    [SerializeField] private Text recordButtonText;

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
        recorder = new Recorder(targetRig);

        userVehicleController = new UserVehicleController(targetRig.GetComponent<CarVehicle>());
        userVehicleController.InitController(targetRig.GetComponent<CarVehicle>());

        // init anim index
        animIndex = 0;

        recordButton.onClick.AddListener(OnStartRecordingPressed);
        recordButtonText.text = "Start Recording";
    }

    override public void UpdateEnvironmentMaster()
    {
        // define animation index
        animIndex = DefineAnimIndex();
        
        // record the state
        recorder.UpdateRecorder();

        // update vehicle controller
        userVehicleController.UpdateController();
    }

    // Defines animation index defined from user input
    private int DefineAnimIndex()
    {
        int output = animIndex;

        if (Input.GetKeyDown(KeyCode.Alpha1)) output = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) output = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) output = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) output = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha5)) output = 4;

        if (animIndex != output)
            Debug.Log("Recording slot is set to: " + output);
        else
            return animIndex;

        return output;
    }

    private void OnStartRecordingPressed()
    {
        recorder.SetRecording(!recorder.GetRecording());
        Debug.Log("Recorder Started");

        recordButtonText.text = "Stop Recording";

        // save data if we stop recording
        if (!recorder.GetRecording())
        {
            recordButtonText.text = "Start Recording";

            recorder.StoreRecordedData(GetInputFieldText(directoryInputField), GetInputFieldText(fileNameInputField));
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
