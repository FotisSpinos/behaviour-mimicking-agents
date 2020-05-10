using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class TrainingEnvironmnetMaster : BaseEnvironmentMaster
{
    [SerializeField] protected AbstractAgentCarController agentCarController;

    [SerializeField] private GameObject physicsBoxPrefub;

    [SerializeField] private AbstractVehicle targetVehicle;

    private EnvironmentActionController actionController;
    private DummyCarController dummyCarController;

    [SerializeField] private AbstractVehicle agentCarVehicle;

    override public void InitEnvironmentMaster()
    {
        // init dummy car
        dummyCarController = new DummyCarController();
        dummyCarController.InitController(targetVehicle);
        dummyCarController.UpdateController();
        dummyCarController.EnableRandomAnimIndex = true;

        // init agent 
        // agentCarControllers = new AbstractAgentCarController[agentCarVehicles.Length];

        agentCarController.InitController(agentCarVehicle);
        agentCarVehicle.GetGameObject().SetActive(true);
    }

    override public void UpdateEnvironmentMaster()
    {
        //Debug.Log("Env update");
        dummyCarController.UpdateController();

        // update environment
        // Academy.Instance.EnvironmentStep();
    }

    override public DummyCarController GetDummyCarController()
    {
        return dummyCarController;
    }
}
