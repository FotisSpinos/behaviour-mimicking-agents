using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordUnityCarEnvMaster : RecordingEnvironmentMaster
{
    [SerializeField] private UnityCar unityVehicle;
    private VehicleController carUserController;

    public override DummyCarController GetDummyCarController()
    {
        throw new System.NotImplementedException();
    }

    public override void InitEnvironmentMaster()
    {
        carUserController = new UnityCarUserController();
        carUserController.InitController(unityVehicle);

        recordButton.onClick.AddListener(OnStartRecordingPressed);
        recordButtonText.text = "Start Recording";
    }

    public override void UpdateEnvironmentMaster()
    {
        carUserController.UpdateController();
    }
}
