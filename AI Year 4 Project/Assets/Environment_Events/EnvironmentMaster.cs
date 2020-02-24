using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMaster : MonoBehaviour
{
    private static EnvironmentMaster instance;
    [SerializeField] private GameObject physicsBoxPrefub;
    [SerializeField] private Rigidbody targetRig;

    EnvironmentActionController actionController;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        // TEMP CODE REMOVE LATER
        actionController = new TestActionController();

        actionController.InitActionController();

        actionController.InitAction();
    }

    // Update is called once per frame
    void Update()
    {
        actionController.UpdateAction();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            actionController.ExcecuteAction();
        }
    }

    public static EnvironmentMaster GetInstance()
    {
        return instance;
    }

    public GameObject GetPhysicsBoxPrefub()
    {
        return physicsBoxPrefub;
    }

    public Rigidbody GetTargetRig()
    {
        return targetRig;
    }
}
