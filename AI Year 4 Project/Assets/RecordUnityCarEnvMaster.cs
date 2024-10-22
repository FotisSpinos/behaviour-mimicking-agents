﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordUnityCarEnvMaster : BaseEnvironmentMaster
{
    [SerializeField] private UnityVehicle unityVehicle;
    private VehicleController carUserController;

    [SerializeField] RecorderUIHandler recorderUIHandler;

    private AbstractRecorder recorder;

    [SerializeField] private string vehicleControllerKey;

    public override AbstractDummyCarController GetDummyCarController()
    {
        throw new System.NotImplementedException();
    }

    public override void InitEnvironmentMaster()
    {
        carUserController = new UserUnityVehicleController();
        carUserController.InitController(unityVehicle);

        recorder = RecroderFactory.CreateRecorder(vehicleControllerKey, unityVehicle, 0.02f); //new UnityCarStateRecorder(unityVehicle, 0.02f);

        recorderUIHandler.StateRecorder = recorder;
    }

    public override void UpdateEnvironmentMaster()
    {
        recorder.UpdateRecorder(Time.deltaTime);
        carUserController.UpdateController();
    }
}
