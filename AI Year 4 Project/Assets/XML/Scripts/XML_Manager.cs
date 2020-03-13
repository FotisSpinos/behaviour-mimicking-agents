using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

public enum Files {Agent, DummyCar}
public class XML_Manager
{
    public static XML_Manager manager;

    public AgentStates agentStates;

    void Awake()
    {
        manager = this;
    }

    public XML_Manager()
    {
        agentStates = new AgentStates();
    }

    public void AddState(Agent_State state)
    {
        agentStates.states.Add(state);
        Debug.Log("State added");
    }

    public List<Agent_State> GetStateList()
    {
        return agentStates.states;
    }

    public void Save(Files saveFile)
    {
        string fileName = GetFile(saveFile);

        XmlSerializer serializer = new XmlSerializer(typeof(AgentStates));
        FileStream stream = new FileStream(Application.dataPath + "/Data/Xml/" + fileName + ".xml", FileMode.Create); //Filemode Create ovewrites

        serializer.Serialize(stream, agentStates);
        stream.Close();

        Debug.Log("Data saved");
    }

    public void Load(Files saveFile)
    {
        string fileName = GetFile(saveFile);

        XmlSerializer serializer = new XmlSerializer(typeof(AgentStates));
        FileStream stream = new FileStream(Application.dataPath + "/Data/Xml/" + fileName + ".xml", FileMode.Open);

        agentStates = serializer.Deserialize(stream) as AgentStates;
        stream.Close();
        
        Debug.Log("Data loaded");
    }

    public string GetFile(Files file)
    {
        string fileName = null;

        switch(file)
        {
            case Files.Agent:
                fileName = "Agent_Data";
                break;       
                
            case Files.DummyCar:
                fileName = "DummyCar_Data";
                break;
        }

        return fileName;
    }

}

[Serializable]
public class Agent_State
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 velocity;
}

[Serializable]
public class AgentStates
{
    public List<Agent_State> states = new List<Agent_State>();
}
