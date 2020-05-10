using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordUnityCarEnvMaster : BaseEnvironmentMaster
{
    [SerializeField] private UnityVehicle unityVehicle;
    private VehicleController carUserController;

    [SerializeField] RecorderUIHandler recorderUIHandler;

    private UnityCarStateRecorder recorder;

    public override DummyCarController GetDummyCarController()
    {
        throw new System.NotImplementedException();
    }

    public override void InitEnvironmentMaster()
    {
        carUserController = new UnityCarUserController();
        carUserController.InitController(unityVehicle);

        recorder = new UnityCarStateRecorder(unityVehicle);

        recorderUIHandler.StateRecorder = recorder;
    }

    public override void UpdateEnvironmentMaster()
    {
        recorder.UpdateRecorder(Time.deltaTime);
        carUserController.UpdateController();
    }
}
