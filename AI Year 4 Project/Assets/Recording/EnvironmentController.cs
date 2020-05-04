using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    protected BaseEnvironmentMaster[] environmentMasters;
    private bool valid;
    [SerializeField] List<string> animDirectories;
    private static EnvironmentController instance;

    [SerializeField] protected float analyticsCaptureRate;
    private float analyticsCaptureRateStore;
    private AgentAnalytics agentAnalytics;

    [SerializeField]private bool training;

    private bool AnalyticsRecorded;

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

        AnalyticsRecorded = false;

        analyticsCaptureRateStore = analyticsCaptureRate;

        if(animDirectories == null)
        {
            animDirectories = new List<string>();
        }

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

        agentAnalytics = new AgentAnalytics(environmentMasters, analyticsCaptureRate);
    }

    private void FixedUpdate()
    {
        if (!valid)
            return;

        // update all BaseEnvironmentMasters 
        foreach (BaseEnvironmentMaster bem in environmentMasters)
        {
            bem.UpdateEnvironmentMaster();
        }

        if (!training)
            return;

        if(analyticsCaptureRate <= 0)
        {
            // update analytics
            AnalyticsRecorded = true;
            agentAnalytics.UpdateAnalytics();
            analyticsCaptureRate = analyticsCaptureRateStore;
        }
        analyticsCaptureRate -= Time.deltaTime;
    }

    private void OnApplicationQuit()
    {
        if(AnalyticsRecorded)
            agentAnalytics.StoreToFile();
    }
}