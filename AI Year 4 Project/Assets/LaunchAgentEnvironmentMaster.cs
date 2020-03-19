using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAgentEnvironmentMaster : BaseEnvironmentMaster
{
    [SerializeField] private GameObject physicsBoxPrefub;
    [SerializeField] private Rigidbody dummyCarRig;

    private ThrowBoxAction throwBoxAction;
    private TeleportAction teleportAction;
    private InvisibleForceAction invisibleForceAction;

    private DummyCarController dummyCarController;

    [SerializeField] private AgentCar agentCar;


    public override DummyCarController GetDummyCarController()
    {
        return dummyCarController;
    }

    public override void InitEnvironmentMaster()
    {
        Rigidbody agentRig = agentCar.GetComponent<Rigidbody>();

        // init dummy car controller
        dummyCarController = new DummyCarController();
        dummyCarController.InitController(dummyCarRig.GetComponent<CarVehicle>());

        // add throw box action
        throwBoxAction = new ThrowBoxAction(physicsBoxPrefub, agentRig);

        // add teleport action
        teleportAction = new TeleportAction(agentCar, 10.0f, 5.0f);

        // add random force action
        invisibleForceAction = new InvisibleForceAction(10000000.0f, agentRig);
    }

    public override void UpdateEnvironmentMaster()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            throwBoxAction.ExcecuteAction();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            teleportAction.ExcecuteAction();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            invisibleForceAction.ExcecuteAction();
        }


        dummyCarController.UpdateController();
    }
}