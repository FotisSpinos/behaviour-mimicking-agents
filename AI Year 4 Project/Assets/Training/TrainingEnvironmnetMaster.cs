using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class TrainingEnvironmnetMaster : BaseEnvironmentMaster
{
    [SerializeField] protected AbstractAgentCarController agentCarController;

    [SerializeField] private AbstractVehicle targetVehicle;

    private AbstractDummyCarController dummyCarController;

    [SerializeField] private AbstractVehicle agentCarVehicle;

    [SerializeField] private string dummyCarKey; 

    override public void InitEnvironmentMaster()
    {
        // init dummy car
        dummyCarController = VehicleControllerFactory.CreateDummyCar(dummyCarKey);
        dummyCarController.InitController(targetVehicle);
        dummyCarController.UpdateController();
        dummyCarController.EnableRandomAnimIndex = true;

        // init agent 
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

    override public AbstractDummyCarController GetDummyCarController()
    {
        return dummyCarController;
    }
}
