using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] private BaseEnvironmentMaster environmentMaster;
    private bool valid;

    // Start is called before the first frame update
    void Start()
    {
        valid = environmentMaster != null;

        if (valid)
            environmentMaster.InitEnvironmentMaster();
        else Debug.LogError("The Base Environment Master is not assigned, please define an environment master from the editor");
    }


    void Update()
    {
        if(valid)
            environmentMaster.UpdateEnvironmentMaster();
    }
}