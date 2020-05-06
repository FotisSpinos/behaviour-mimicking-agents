using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnalytics
{
    private float avairageFitness;

    private SerializableFitnessValues serializableFitnessValues;

    private BaseEnvironmentMaster[] environmentMasters;

    public AgentAnalytics(BaseEnvironmentMaster[] environmentMasters, float analyticsCaptureRate)
    {
        this.environmentMasters = environmentMasters;
        serializableFitnessValues = new SerializableFitnessValues();

        serializableFitnessValues.captureRate = analyticsCaptureRate;
    }

    public void StoreToFile()
    {
        string path = PathBuilder.CreateXmlFilePath(PathBuilder.FileTypes.OBJ_RECORD_DATA, 
            "fitness",
            "Fitness Values",
            true);


        //string date = DateTime.Now.ToString("dd-MM-yyyy-HHmm");
        XmlReadWrite.GetInstance().SaveFitnessValues(path, serializableFitnessValues);
    }

    public void UpdateAnalytics()
    {
        if (environmentMasters.Length == 0)
            return;

        float fitnessSum = 0;
        foreach(BaseEnvironmentMaster envMast in environmentMasters)
        {
            if(envMast.GetAgentCarController() != null)
                fitnessSum += envMast.GetAgentCarController().Fitness;
        }

        avairageFitness = fitnessSum / environmentMasters.Length;

        serializableFitnessValues.fitnessValues.Add(avairageFitness);
    }
}
