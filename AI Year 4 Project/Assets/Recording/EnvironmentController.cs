using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    private BaseEnvironmentMaster[] environmentMasters;
    private bool valid;

    // Start is called before the first frame update
    void Awake()
    {
        valid = true;

        // Destroy other environment controllers
        EnvironmentController[] envControllers = FindObjectsOfType<EnvironmentController>();
        for(int i = 1; i < envControllers.Length; i++)
        {
            if(envControllers[i] != this)
            {
                Destroy(envControllers[i].gameObject);
            }
        }

        // find all BaseEnvironmentMaster
        environmentMasters = FindObjectsOfType<BaseEnvironmentMaster>();
        if(environmentMasters == null || environmentMasters.Length == 0)
        {
            Debug.LogError("This scene does not contain environment masters!");
            valid = false;
            return;
        }

        // initialize all environments
        foreach(BaseEnvironmentMaster bem in environmentMasters)
        {
            bem.InitEnvironmentMaster();
        }
    }

    void Update()
    {
        if (!valid)
            return;

        // update all BaseEnvironmentMasters 
        foreach (BaseEnvironmentMaster bem in environmentMasters)
        {
            bem.UpdateEnvironmentMaster();
        }
    }
}