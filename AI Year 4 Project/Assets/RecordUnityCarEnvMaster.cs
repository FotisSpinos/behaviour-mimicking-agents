using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordUnityCarEnvMaster : BaseEnvironmentMaster
{
    [SerializeField] private UnityVehicle unityVehicle;
    private VehicleController carUserController;

    [SerializeField] RecorderUIHandler recorderUIHandler;

    private UnityCarStateRecorder recorder;

    public override AbstractDummyCarController GetDummyCarController()
    {
        throw new System.NotImplementedException();
    }

    public override void InitEnvironmentMaster()
    {
        carUserController = new UserUnityVehicleController();
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
