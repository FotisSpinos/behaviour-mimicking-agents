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

    // The ui handler controlling the recorder 
    [SerializeField] RecorderUIHandler recorderUIHandler;

    // An instance to the recording vehicle
    [SerializeField] private AbstractVehicle recordingVeh;

    [SerializeField] private string userControllerKey;

    // default constructor
    public RecordingEnvironmentMaster() {}

    public override AbstractDummyCarController GetDummyCarController()
    {
        return null;
    }

    override public void InitEnvironmentMaster()
    {
        // make a recorder instance
        recorder = new StateRecorder(recordingVeh, 0.02f);

        userVehicleController = VehicleControllerFactory.CreateUserVehicleController(userControllerKey);
        userVehicleController.InitController(recordingVeh);

        recorderUIHandler.StateRecorder = recorder;
    }

    override public void UpdateEnvironmentMaster()
    {   
        // record the state
        recorder.UpdateRecorder(Time.deltaTime);

        // update vehicle controller
        userVehicleController.UpdateController();
    }
}
