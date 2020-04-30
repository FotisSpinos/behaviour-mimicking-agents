using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XML_Test : MonoBehaviour
{
    private XmlReadWrite manager_XML;

    public Agent_State testState;

    public List<Agent_State> loadedStates;

    public Files file;

    public List<float> lst_FitVals = new List<float>();


    void Start()
    {
        manager_XML = XmlReadWrite.GetInstance();

        lst_FitVals.Add(1);
        lst_FitVals.Add(1.3f);
        lst_FitVals.Add(7);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            manager_XML.Save(file);
        }        
        
        if(Input.GetKeyUp(KeyCode.L))
        {
            manager_XML.Load(file);
            loadedStates = manager_XML.GetStateList();
        }        
        
        if(Input.GetKeyUp(KeyCode.A))
        {
            manager_XML.AddState(testState);
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            manager_XML.AddFitLst(lst_FitVals);
            manager_XML.SaveFitness();
        }

    }
}
