﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XML_Test : MonoBehaviour
{
    public XML_Manager manager_XML;

    public Agent_State testState;

    public List<Agent_State> loadedStates;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            manager_XML.Save();
        }        
        if(Input.GetKeyUp(KeyCode.L))
        {
            manager_XML.Load();
            loadedStates = manager_XML.GetStateList();
        }        
        if(Input.GetKeyUp(KeyCode.A))
        {
            manager_XML.AddState(testState);
        }
    }
}