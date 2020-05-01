using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesManager
{
    private static StatesManager instance = null;
    private List<SerializableAgentStates> sas;


    public static StatesManager GetInstance()
    {
        if (instance == null)
            instance = new StatesManager();

        return instance;
    }

    private StatesManager()
    {
        sas = new List<SerializableAgentStates>();
    }

    public void AddState(int index, AgentState state)
    {
        sas[index].states.Add(state);
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

    /* SETERS */
    public void SetAgentStates(int index, List<AgentState> states)
    {
        sas[index].states = states;
    }

    public void SetSerializableAgentStates(int index, SerializableAgentStates serializableAgentStates)
    {
        sas[index] = serializableAgentStates;
    }

    public void ClearData(int index)
    {
        sas[index].states.Clear();
    }
}
