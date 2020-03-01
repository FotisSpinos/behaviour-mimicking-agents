/*
 * Contains Scene information
 * Allows to access objects significant for testing and training scenes
 * Only one Game Object with this component should exist in a scene
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMaster : MonoBehaviour
{
    private static EnvironmentMaster instance;

    [SerializeField] private GameObject physicsBoxPrefub;
    [SerializeField] private Rigidbody targetRig;

    [SerializeField] private bool record;

    EnvironmentActionController actionController;
    private Recorder recorder;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        // TEMP CODE REMOVE LATER
        actionController = new SimpleActionController();

        actionController.InitActionController();
        actionController.InitAction();

        recorder = new Recorder(targetRig);
    }

    // Update is called once per frame
    void Update()
    {
        actionController.UpdateAction();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            actionController.ExcecuteAction();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            recorder.SetRecording(!recorder.GetRecording());
        }
        recorder.UpdateRecorder();
    }

    public static EnvironmentMaster GetInstance()
    {
        if(instance == null)
        {
            GameObject envMasterGo = new GameObject();
            instance = envMasterGo.AddComponent<EnvironmentMaster>();
        }
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
