using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActionController : EnvironmentActionController
{
    private EnvironmentAction envAction;

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
        envAction = new ThrowBoxAction(EnvironmentMaster.GetInstance().GetPhysicsBoxPrefub(),
        EnvironmentMaster.GetInstance().GetTargetRig());
    }
}
