using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

public enum Files {CarPath_1, CarPath_2, CarPath_3, CarPath_4, CarPath_5}
public class XML_Manager: MonoBehaviour
{
    public static XML_Manager manager;

    public AgentStates agentStates;

    public List<AgentStates> pathsList = new List<AgentStates>();
    //// manager.pathsList[index].states;

    void Awake()
    {
        manager = this;
        pathsList = LoadAll();
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

    List<AgentStates> LoadAll()
    {
        List<AgentStates> loadList = new List<AgentStates>();

        foreach (Files slot in Enum.GetValues(typeof(Files)))
        {
            Load(slot);
            loadList.Add(agentStates);
        }

        return loadList;
    }


    public string GetFile(Files file)
    {
        string fileName = null;

        switch(file)
        {
            case Files.CarPath_1:
                fileName = "CarPath_1_Data";
                break;       
                
            case Files.CarPath_2:
                fileName = "CarPath_2_Data";
                break;            
            
            case Files.CarPath_3:
                fileName = "CarPath_3_Data";
                break;            
            
            case Files.CarPath_4:
                fileName = "CarPath_4_Data";
                break;            
            
            case Files.CarPath_5:
                fileName = "CarPath_5_Data";
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
