using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

public enum Files {CarPath_1 = 0, CarPath_2 = 1, CarPath_3 = 2, CarPath_4 = 3, CarPath_5 = 4}
public class XmlReadWrite
{
    public static XmlReadWrite manager;

    public AgentStates agentStates;

    public List<AgentStates> pathsList = new List<AgentStates>();
    //// manager.pathsList[index].states;

    public static XmlReadWrite GetInstance()
    {
        if(manager == null)
        {
            manager = new XmlReadWrite();
        }
        return manager;
    }

    public XmlReadWrite()
    {
        agentStates = new AgentStates();
        pathsList = LoadAll();
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
        if (pathsList != null)
        {
            UpdatePathList(saveFile);
            Debug.Log("Paths updated");
        }
    }

    public void Save(String fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AgentStates));
        FileStream stream = new FileStream(Application.dataPath + "/Data/Xml/" + fileName + ".xml", FileMode.Create); //Filemode Create ovewrites

        serializer.Serialize(stream, agentStates);
        stream.Close();

        Debug.Log("Data saved");
        if (pathsList != null)
        {
            //UpdatePathList(saveFile);
            //Debug.Log("Paths updated");
        }
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

    public void UpdatePathList(Files updateFile)
    {
        Load(updateFile);
        pathsList[(int)updateFile] = agentStates;
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
