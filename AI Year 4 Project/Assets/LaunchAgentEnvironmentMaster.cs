using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAgentEnvironmentMaster : BaseEnvironmentMaster
{
    [SerializeField] private GameObject physicsBoxPrefub;
    [SerializeField] private Rigidbody dummyCarRig;

    private EnvironmentActionController actionController;
    private DummyCarController dummyCarController;

    [SerializeField] private AgentCar agentCar;


    public override DummyCarController GetDummyCarController()
    {
        return dummyCarController;
    }

    public override void InitEnvironmentMaster()
    {
        // init dummy car controller
        dummyCarController = new DummyCarController();
        dummyCarController.InitController(dummyCarRig.GetComponent<CarVehicle>());

        actionController = new SimpleActionController(new ThrowBoxAction(physicsBoxPrefub, 
            agentCar.gameObject.GetComponent<Rigidbody>()));
    }

    public override void UpdateEnvironmentMaster()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            actionController.ExcecuteAction();
        }

        actionController.UpdateAction();

        dummyCarController.UpdateController();
    }
}
