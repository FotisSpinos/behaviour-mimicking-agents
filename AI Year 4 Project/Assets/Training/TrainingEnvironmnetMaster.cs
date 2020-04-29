using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class TrainingEnvironmnetMaster : BaseEnvironmentMaster
{
    [SerializeField] private GameObject physicsBoxPrefub;

    [SerializeField] private CarVehicle targetVehicle;

    private EnvironmentActionController actionController;
    private DummyCarController dummyCarController;

    private VehicleController agentVehicleController;

    [SerializeField] private CarVehicle agentCarVehicle;
    [SerializeField] private AgentCarController agentCarController;

    override public void InitEnvironmentMaster()
    {
        // init dummy car
        dummyCarController = new DummyCarController();
        dummyCarController.InitController(targetVehicle);

        // AutomaticSteppingEnabled 
        Academy.Instance.AutomaticSteppingEnabled = false;

        // init agent car
        agentCarController.InitController(agentCarVehicle);
    }

    override public void UpdateEnvironmentMaster()
    {
        dummyCarController.UpdateController();

        // update environment
        Academy.Instance.EnvironmentStep();
    }

    override public DummyCarController GetDummyCarController()
    {
        return dummyCarController;
    }
}
