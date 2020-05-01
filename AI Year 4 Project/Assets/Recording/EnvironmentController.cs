using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    protected BaseEnvironmentMaster[] environmentMasters;
    private bool valid;
    [SerializeField] List<string> animDirectories;
    private static EnvironmentController instance;

    [SerializeField] protected float analyticsRefreshRate;
    private float analyticsRefreashRateStore;
    private AgentAnalytics agentAnalytics;

    public static EnvironmentController GetInstance()
    {
        if (instance == null)
            instance = new EnvironmentController();
        return instance;
    }

    public EnvironmentController() 
    {}

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        valid = true;

        analyticsRefreashRateStore = analyticsRefreshRate;

        // Load animations
        foreach (string directory in animDirectories)
        {
            XmlReadWrite.GetInstance().LoadAllXmlFiles(directory);
        }

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


        agentAnalytics = new AgentAnalytics(environmentMasters);
    }

    void FixedUpdate()
    {
        if (!valid)
            return;

        // update all BaseEnvironmentMasters 
        foreach (BaseEnvironmentMaster bem in environmentMasters)
        {
            bem.UpdateEnvironmentMaster();
        }

        if(analyticsRefreshRate <= 0)
        {
            // update analytics
            agentAnalytics.UpdateAnalytics();
            agentAnalytics.StoreToFile();
            analyticsRefreshRate = analyticsRefreashRateStore;
        }
        analyticsRefreshRate -= Time.deltaTime;
    }
}