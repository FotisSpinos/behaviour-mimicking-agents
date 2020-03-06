using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleActionController : EnvironmentActionController
{
    //environment action handled by the environment controller
    private EnvironmentAction envAction;
    private GameObject physicsBoxPrefub;
    private Rigidbody targetRig;

    public SimpleActionController(GameObject physicsBoxPrefub, Rigidbody targetRig)
    {
        this.physicsBoxPrefub = physicsBoxPrefub;
        this.targetRig = targetRig;
    }

    public void ExcecuteAction()
    {
        envAction.ExcecuteAction();
    }

    public void UpdateAction()
    {
        envAction.UpdateAction();
    }

    public void InitAction()
    {
        envAction.InitAction();
    }

    public void InitActionController()
    {
        envAction = new ThrowBoxAction(physicsBoxPrefub,
        targetRig);
    }
}
