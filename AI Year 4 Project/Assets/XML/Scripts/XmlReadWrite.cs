using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;
using UnityStandardAssets.Vehicles.Car;

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

    public void SaveStates(string path, SerializableAgentStates agentStates)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SerializableAgentStates));

        FileInfo file = new FileInfo(path);

        FileMode currentMode = FileMode.CreateNew;
        if (file.Exists)
            currentMode = FileMode.Open;

        file.Directory.Create();
        FileStream stream = new FileStream(path, currentMode); //Filemode Create ovewrites

        serializer.Serialize(stream, agentStates);
        stream.Close();

        Debug.Log("Data saved");
    }

    public void SaveImpactPoints(string path, SerializableImpactPoints impactPoints)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SerializableImpactPoints));

        FileInfo file = new FileInfo(path);

        FileMode currentMode = FileMode.CreateNew;
        if (file.Exists)
            currentMode = FileMode.Open;

        file.Directory.Create();
        FileStream stream = new FileStream(path, currentMode); //Filemode Create ovewrites

        serializer.Serialize(stream, impactPoints);
        stream.Close();

        Debug.Log("Data saved");
    }

    public void SaveCarControllerAtributes(string path, SerCarControllerAtr atr)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SerCarControllerAtr));

        FileInfo file = new FileInfo(path);

        FileMode currentMode = FileMode.CreateNew;
        if (file.Exists)
            currentMode = FileMode.Open;

        file.Directory.Create();
        FileStream stream = new FileStream(path, currentMode); //Filemode Create ovewrites

        serializer.Serialize(stream, atr);
        stream.Close();

        Debug.Log("Data saved");
    }

    public void SaveFitnessValues(string path, SerializableFitnessValues serializableFitnessValues)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SerializableFitnessValues));

        FileInfo file = new FileInfo(path);

        FileMode currentMode = FileMode.CreateNew;
        if (file.Exists)
            currentMode = FileMode.Open;

        file.Directory.Create();
        FileStream stream = new FileStream(path, currentMode); //Filemode Create ovewrites

        serializer.Serialize(stream, serializableFitnessValues);
        stream.Close();

        Debug.Log("Fitness value stored: " + serializableFitnessValues.fitnessValues);
    }

    public void LoadAllStates(string directoryName)
    {
        string path = PathBuilder.CreateDirectoryPath(PathBuilder.FileTypes.ANIMATION_DATA, directoryName);
        string[] fileEntries = Directory.GetFiles(path);
        foreach (string fileName in fileEntries)
        {
            if(fileName.EndsWith(".xml"))
                LoadStates(fileName);
        }
    }

    public void LoadAllUnityVehicleStates(string directoryName)
    {
        string path = PathBuilder.CreateDirectoryPath(PathBuilder.FileTypes.ANIMATION_DATA, directoryName);
        string[] fileEntries = Directory.GetFiles(path);
        foreach (string fileName in fileEntries)
        {
            if (fileName.EndsWith(".xml"))
                LoadUnityVehicleStates(fileName);
        }
    }

    public void LoadUnityVehicleStates(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SerCarControllerAtr));

        FileInfo file = new FileInfo(path);

        if (!file.Exists)
        {
            Debug.Log("File does not exist: " + path);
            return;
        }

        StreamReader reader = new System.IO.StreamReader(path);

        try
        {
            LoadedVehicleStates.GetInstance().AddSerializableAgentState(serializer.Deserialize(reader) as SerCarControllerAtr);
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

    public void LoadStates(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SerializableAgentStates));

        FileInfo file = new FileInfo(path);

        if (!file.Exists)
        {
            Debug.Log("File does not exist: " + path);
            return;
        }

        StreamReader reader = new System.IO.StreamReader(path);

        try
        {
            LoadedStates.GetInstance().AddSerializableAgentState(serializer.Deserialize(reader) as SerializableAgentStates);
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
}

[Serializable]
public class SerializableFitnessValues
{
    public List<float> fitnessValues = new List<float>();
    public float captureRate = 0.0f;
}

[Serializable]
public class SerializableImpactPoints
{
    public List<float> impactPoitns = new List<float>();
    public float captureRate = 0.0f;
}

[Serializable]
public class SerCarControllerAtr
{
    public List<CarController.Atributes> carContrAtr = new List<CarController.Atributes>();
}