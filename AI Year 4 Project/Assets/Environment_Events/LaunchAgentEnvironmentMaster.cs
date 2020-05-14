using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAgentEnvironmentMaster : BaseEnvironmentMaster
{
    [SerializeField] private GameObject physicsBoxPrefub;
    [SerializeField] private Rigidbody dummyCarRig;

    [SerializeField] private Rigidbody agentRig;

    private ThrowBoxAction throwBoxAction;
    private TeleportAction teleportAction;
    private InvisibleForceAction invisibleForceAction;

    [SerializeField] private AbstractDummyCarController dummyCarController;

    [SerializeField] private AbstractAgentCarController agentCarController;

    private StateRecorder agentRecorder;
    private StateRecorder dummyCarRecorder;

    public override AbstractDummyCarController GetDummyCarController()
    {
        return dummyCarController;
    }

    public UnityDummyCarController GetUnityDummyCar()
    {
        return null;
    }

    public override void InitEnvironmentMaster()
    {
        Rigidbody agentRig = agentCarController.GetComponent<Rigidbody>();

        // init dummy car controller
        dummyCarController = new SimpleDummyCarController();
        dummyCarController.InitController(dummyCarRig.GetComponent<CarVehicle>());
        dummyCarController.EnableRandomAnimIndex = false;
        dummyCarController.UpdateController();

        // add throw box action
        throwBoxAction = new ThrowBoxAction(physicsBoxPrefub, agentRig);

        // add teleport action
        teleportAction = new TeleportAction(agentCarController, 10.0f, 5.0f);

        // add random force action
        invisibleForceAction = new InvisibleForceAction(1000.0f, agentRig);

        // initialize agent recorder and start recording
        agentRecorder = new StateRecorder(agentRig.GetComponent<CarVehicle>(), 0.02f);
        agentRecorder.SetRecording(true);

        // initialize dummy recorder and start recording
        dummyCarRecorder = new StateRecorder(dummyCarRig.GetComponent<CarVehicle>(), 0.02f);
        dummyCarRecorder.SetRecording(true);

        // activate agent car
        agentCarController.gameObject.SetActive(true);
        agentCarController.InitController(agentRig.GetComponent<CarVehicle>());
    }

    public override void UpdateEnvironmentMaster()
    {
        // excecute action on user press
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            throwBoxAction.ExecuteAction();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            teleportAction.ExecuteAction();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            invisibleForceAction.ExecuteAction();
        }

        // update dummy car controller
        dummyCarController.UpdateController();

        // update agent recorder
        agentRecorder.UpdateRecorder(0.02f);

        // update dummy car recorder
        dummyCarRecorder.UpdateRecorder(0.02f);
    }

    public void OnApplicationQuit()
    {
        agentRecorder.StoreStates("Agent States", "Stored States", PathBuilder.FileTypes.OBJ_RECORD_DATA);
        dummyCarRecorder.StoreStates("Dummy Car States", "Stored States", PathBuilder.FileTypes.OBJ_RECORD_DATA);

        agentRecorder.StoreImpactPoints("Agent Impact Poitns", "Impact Points", PathBuilder.FileTypes.OBJ_RECORD_DATA);
    }
}