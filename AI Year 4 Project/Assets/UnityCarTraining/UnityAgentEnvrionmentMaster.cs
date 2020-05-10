using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityAgentEnvrionmentMaster : BaseEnvironmentMaster
{
    [SerializeField] protected AbstractAgentCarController agentCarController;

    [SerializeField] private GameObject physicsBoxPrefub;

    [SerializeField] private UnityVehicle targetVehicle;

    private EnvironmentActionController actionController;
    private UnityDummyCar dummyCarController;

    [SerializeField] private AbstractVehicle agentCarVehicle;

    public override DummyCarController GetDummyCarController()
    {
        throw new System.NotImplementedException();
    }

    public UnityDummyCar GetUnityDummyCar()
    {
        return dummyCarController;
    }

    override public void InitEnvironmentMaster()
    {
        // init dummy car
        dummyCarController = new UnityDummyCar(targetVehicle);
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
}
