using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleActionController : EnvironmentActionController
{
    //environment action handled by the environment controller
    private EnvironmentAction envAction;
    private GameObject physicsBoxPrefub;
    private Rigidbody targetRig;

    public SimpleActionController(EnvironmentAction envAction)
    {
        this.envAction = envAction;
    }

    public void ExcecuteAction()
    {
        envAction.ExecuteAction();
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
    }
}
