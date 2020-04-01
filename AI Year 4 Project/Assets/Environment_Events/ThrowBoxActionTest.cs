using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBoxActionTest : MonoBehaviour
{
    [SerializeField] private float excecutionRate = 5.0f;
    private float excecutionRateStore;

    //[SerializeField] private ThrowBoxAction envAction; 
    private EnvironmentAction envAction;

    private void Start()
    {
        excecutionRateStore = excecutionRate;

        envAction.InitAction();
    }


    void Update()
    {
        if(excecutionRate <= 0.0f)
        {
            excecutionRate = excecutionRateStore;

            envAction.ExecuteAction();
        }

        envAction.UpdateAction();

        excecutionRate -= Time.deltaTime;
    }
}
