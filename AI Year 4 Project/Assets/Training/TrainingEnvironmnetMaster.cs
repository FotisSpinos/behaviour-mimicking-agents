using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingEnvironmnetMaster : BaseEnvironmentMaster
{
    [SerializeField] private GameObject physicsBoxPrefub;
    [SerializeField] private Rigidbody targetRig;

    private EnvironmentActionController actionController;
    private DummyCarController dummyCarController;

    override public void InitEnvironmentMaster()
    {
        // load the data
        dummyCarController = new DummyCarController();
        dummyCarController.InitController(targetRig.GetComponent<CarVehicle>());
    }

    override public void UpdateEnvironmentMaster()
    {
        dummyCarController.UpdateController();
    }

    override public DummyCarController GetDummyCarController()
    {
        return dummyCarController;
    }
}
