using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

public class XmlReadWrite
{
    public static XmlReadWrite manager;

    public static XmlReadWrite GetInstance()
    {
        if(manager == null)
        {
            manager = new XmlReadWrite();
        }
        return manager;
    }

    public void SaveStates(string directory, string fileName, SerializableAgentStates agentStates)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SerializableAgentStates));

        FileInfo file = new FileInfo(Application.dataPath + "/Data/" + directory + "/" + fileName + ".xml");

        FileMode currentMode = FileMode.CreateNew;
        if (file.Exists)
            currentMode = FileMode.Open;

        file.Directory.Create();
        FileStream stream = new FileStream(Application.dataPath + "/Data/" + directory + "/" + fileName + ".xml", currentMode); //Filemode Create ovewrites

        serializer.Serialize(stream, agentStates);
        stream.Close();

        Debug.Log("Data saved");
    }

    public void SaveFitnessValues(string directory, string fileName, SerializableFitnessValues serializableFitnessValues)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SerializableFitnessValues));

        FileInfo file = new FileInfo(Application.dataPath + "/Data/" + directory + "/" + fileName + ".xml");

        FileMode currentMode = FileMode.CreateNew;
        if (file.Exists)
            currentMode = FileMode.Open;

        file.Directory.Create();
        FileStream stream = new FileStream(Application.dataPath + "/Data/" + directory + "/" + fileName + ".xml", currentMode); //Filemode Create ovewrites

        serializer.Serialize(stream, serializableFitnessValues);
        stream.Close();

        Debug.Log("Fitness value stored: " + serializableFitnessValues.fitnessValues);
    }

    public void LoadAllXmlFiles(string directoryName)
    {
        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/Data/" + directoryName);
        foreach (string fileName in fileEntries)
        {
            if(fileName.EndsWith(".xml"))
                Load(fileName);
        }
    }

    public void Load(string path)
    {

        XmlSerializer serializer = new XmlSerializer(typeof(SerializableAgentStates));

        FileInfo file = new FileInfo(path);

        if (!file.Exists)
        {
            Debug.Log("File does not exist: " + path);
            return;
        }

        //FileStream stream = new FileStream(path, FileMode.Open); //Filemode Create ovewrites
        StreamReader reader = new System.IO.StreamReader(path);

        try
        {
            StatesManager.GetInstance().AddSerializableAgentState(serializer.Deserialize(reader) as SerializableAgentStates);
        }
        catch (Exception c)
        {
            Debug.LogWarning("Exception occured while reading file: " + path);
            Debug.LogWarning(c.ToString());
            reader.Close();
            return;
        }

        reader.Close();

        Debug.Log("Data loaded from: " + file.FullName);

    }

    private void CreateDirectory(string directoryName)
    {
        bool directoryExists = System.IO.Directory.Exists(Application.dataPath + "/Data/" + directoryName);

        if (!directoryExists)
        {
            Directory.CreateDirectory(Application.dataPath + "/Data/" + directoryName);
        }
    }

    private void CreateFile(string directoryName, string fileName)
    {
        bool fileExists = System.IO.File.Exists(Application.dataPath + "/Data/" + directoryName + "/" + fileName + ".xml");

        if (!fileExists)
        {
            File.Create(Application.dataPath + "/Data/" + directoryName + "/" + fileName + ".xml");
        }
    }
}


[Serializable]
public struct AgentState
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 velocity;
}

[Serializable]
public class SerializableAgentStates
{
    public List<AgentState> states = new List<AgentState>();
    public float captureRate = 0.0f;
    public List<float> impactPoitns = new List<float>();
}

[Serializable]
public class SerializableFitnessValues
{
    public List<float> fitnessValues = new List<float>();
    public float captureRate;
}