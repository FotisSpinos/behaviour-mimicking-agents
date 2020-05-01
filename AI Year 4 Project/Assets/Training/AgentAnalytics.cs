using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnalytics
{
    private float avairageFitness;

    private SerializableFitnessValues serializableFitnessValues;

    private BaseEnvironmentMaster[] environmentMasters;

    public AgentAnalytics(BaseEnvironmentMaster[] environmentMasters)
    {
        this.environmentMasters = environmentMasters;
        serializableFitnessValues = new SerializableFitnessValues();
    }

    public void StoreToFile()
    {
        XmlReadWrite.GetInstance().SaveFitnessValues("Fitness Values", "FirstTest", serializableFitnessValues);
    }

    public void UpdateAnalytics()
    {
        if (environmentMasters.Length == 0)
            return;

        float fitnessSum = 0;
        foreach(BaseEnvironmentMaster envMast in environmentMasters)
        {
            fitnessSum += envMast.GetAgentCarController().Fitness;
        }

        avairageFitness = fitnessSum / environmentMasters.Length;

        serializableFitnessValues.fitnessValues.Clear();
        serializableFitnessValues.fitnessValues.Add(avairageFitness);
    }
}
