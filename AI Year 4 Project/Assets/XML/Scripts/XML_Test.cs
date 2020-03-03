using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XML_Test : MonoBehaviour
{
    public XML_Manager manager_XML;

    public Agent_State testState;

    public List<Agent_State> loadedStats = new List<Agent_State>();

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("A");
            manager_XML.AddState(testState);
        }          
        
        if(Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("S");
            manager_XML.Save();
        }        
        
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L");
            manager_XML.Load();
            loadedStats = manager_XML.GetList_AgentStates();
        }
    }
}
