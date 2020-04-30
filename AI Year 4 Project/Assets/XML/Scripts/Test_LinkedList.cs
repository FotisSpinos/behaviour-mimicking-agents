using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

public class Test_LinkedList : MonoBehaviour
{
    public Linked testLink;
    public string fileName;
    // Start is called before the first frame update
    void Start()
    {
        testLink.linkInt.AddLast(1);
        testLink.linkInt.AddLast(3);
        testLink.linkInt.AddLast(878);
        testLink.linkInt.AddLast(6);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            if(string.IsNullOrWhiteSpace(fileName))
            {
                Debug.Log("Enter File name");
            }
            else
            {
                Save();
            }
            
        }        
        
        if (Input.GetKeyUp(KeyCode.L))
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Debug.Log("Enter File name");
            }
            else
            {
                Load();
            }
        }
    }

    void Save()
    {

        XmlSerializer serializer = new XmlSerializer(typeof(Linked));
        FileStream stream = new FileStream(Application.dataPath + "/XML/" + fileName + ".xml", FileMode.Create); //Filemode Create ovewrites

        serializer.Serialize(stream, testLink);
        stream.Close();

        Debug.Log("Data saved");
    }

    void Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Linked));
        FileStream stream = new FileStream(Application.dataPath + "/XML/" + fileName + ".xml", FileMode.Open);

        testLink = serializer.Deserialize(stream) as Linked;
        stream.Close();

        Debug.Log("Data loaded");

    }

}
[Serializable]
public class Linked
{
    public LinkedList<int> linkInt = new LinkedList<int>();
}