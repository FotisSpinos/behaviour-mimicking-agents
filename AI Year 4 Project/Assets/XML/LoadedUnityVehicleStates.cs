using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadedVehicleStates
{
    private static LoadedVehicleStates instance;
    private List<SerCarControllerAtr> sas;

    public static LoadedVehicleStates GetInstance()
    {
        if (instance == null)
            instance = new LoadedVehicleStates();

        return instance;
    }

    private LoadedVehicleStates()
    {
        sas = new List<SerCarControllerAtr>();
    }

    public void AddSerializableAgentState(SerCarControllerAtr sas)
    {
        this.sas.Add(sas);
    }


    /* GETERS */
    public List<SerCarControllerAtr> GetSerCarControllerAtrs()
    {
        return sas;
    }
}