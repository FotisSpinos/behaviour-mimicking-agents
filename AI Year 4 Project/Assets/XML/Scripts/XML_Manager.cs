using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

public class XML_Manager : MonoBehaviour
{
    public static XML_Manager manager;

    public AgentStates agentStates;
    public Agent_State agent;

    public string fileName;

    void Awake()
    {
        manager = this;
    }

    public void AddState(Agent_State state)
    {
        agentStates.states.Add(state);
    }

    public List<Agent_State> GetList_AgentStates()
    {
        return agentStates.states;
    }

    public void Save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AgentStates));
        FileStream stream = new FileStream(Application.dataPath + "/Data/Xml/" + fileName + ".xml", FileMode.Create); //Filemode Create ovewrites

        serializer.Serialize(stream, agentStates);
        stream.Close();

        Debug.Log("Save");
    }

    public void Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AgentStates));
        FileStream stream = new FileStream(Application.dataPath + "/Data/Xml/" + fileName + ".xml", FileMode.Open);

        agentStates = serializer.Deserialize(stream) as AgentStates;
        stream.Close();
        
        Debug.Log("Load");
    }
}

[Serializable]
public class Agent_State
{
    public Vector3 Position;
    public Vector3 rotation;
    public Vector3 velocity;
}

[Serializable]
public class AgentStates
{
    public List<Agent_State> states = new List<Agent_State>();
}
