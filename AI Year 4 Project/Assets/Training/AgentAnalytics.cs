using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnalytics
{
    private float avairageFitness;

    private SerializableFitnessValues serializableFitnessValues;

    private AbstractAgentCarController[] abstractAgentCarController;

    public AgentAnalytics(float analyticsCaptureRate)
    {
        abstractAgentCarController = GameObject.FindObjectsOfType<AbstractAgentCarController>();
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
        if (abstractAgentCarController.Length == 0)
            return;

        float fitnessSum = 0;
        foreach (AbstractAgentCarController agent in abstractAgentCarController)
        {
            fitnessSum += agent.Fitness;
        }

        avairageFitness = fitnessSum / abstractAgentCarController.Length;

        serializableFitnessValues.fitnessValues.Add(avairageFitness);
    }
}
