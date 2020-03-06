/*
 * Contains Scene information
 * Allows to access objects significant for testing and training scenes
 * Only one Game Object with this component should exist in a scene
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingEnvironmentMaster : BaseEnvironmentMaster
{
    private static EnvironmentMaster instance;
    
    [SerializeField] private GameObject physicsBoxPrefub;
    [SerializeField] private Rigidbody targetRig;

    private bool record;

    private EnvironmentActionController actionController;
    private Recorder recorder;

    private VehicleController vehicleController;

    public RecordingEnvironmentMaster()
    {

    }

    public static EnvironmentMaster GetInstance()
    {
        if(instance == null)
        {
            GameObject envMasterGo = new GameObject();
        }
        return instance;
    }

    override public void InitEnvironmentMaster()
    {       
        actionController = new SimpleActionController(physicsBoxPrefub, targetRig);

        actionController.InitActionController();
        actionController.InitAction();
        

        // make a recorder instance
        recorder = new Recorder(targetRig);

        vehicleController = new UserVehicleController(targetRig.GetComponent<AgentCar>());
        vehicleController.InitController(targetRig.GetComponent<AgentCar>());
    }

    override public void UpdateEnvironmentMaster()
    {
        // excecute action if we press space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            actionController.ExcecuteAction();
        }
        
        // when we press r start recording
        if (Input.GetKeyDown(KeyCode.R))
        {
            recorder.SetRecording(!recorder.GetRecording());

            // save data if we stop recording
            if (!recorder.GetRecording())
                recorder.StoreRecordedData();
        }
        
        // update action controller
        actionController.UpdateAction();

        // record the state
        recorder.UpdateRecorder();

        // update vehicle controller
        vehicleController.UpdateController();
    }
}
