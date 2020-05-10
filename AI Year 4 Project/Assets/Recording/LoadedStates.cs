using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadedStates
{
    private static LoadedStates instance = null;
    private List<SerializableAgentStates> sas;

    public static LoadedStates GetInstance()
    {
        if (instance == null)
            instance = new LoadedStates();

        return instance;
    }

    private LoadedStates()
    {
        sas = new List<SerializableAgentStates>();
    }

    public void AddSerializableAgentState(SerializableAgentStates sas)
    {
        this.sas.Add(sas);
    }

    /* GETERS */
    public List<AgentState> GetAgentStateStates(int index)
    {
        return sas[index].states;
    }

    public SerializableAgentStates GetSerializableAgentState(int index)
    {
        return sas[index];
    }

    public List<SerializableAgentStates> GetSerializableAgentStates()
    {
        return sas;
    }
}
