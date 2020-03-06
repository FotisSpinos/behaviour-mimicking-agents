using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private BaseEnvironmentMaster environmentMaster;

    // Start is called before the first frame update
    void Start()
    {
        environmentMaster.InitEnvironmentMaster();
    }

    // Update is called once per frame
    void Update()
    {
        environmentMaster.UpdateEnvironmentMaster();
    }
}
