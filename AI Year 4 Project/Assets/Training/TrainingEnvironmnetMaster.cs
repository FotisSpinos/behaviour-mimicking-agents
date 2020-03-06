using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingEnvironmnetMaster : BaseEnvironmentMaster
{
    [SerializeField] private GameObject physicsBoxPrefub;
    [SerializeField] private Rigidbody targetRig;

    private EnvironmentActionController actionController;
    private VehicleController dummyCar;

    private XML_Manager xmlManager;

    override public void InitEnvironmentMaster()
    {
        // load the datat
        dummyCar = new TrainingVehicleController();
        dummyCar.InitController(targetRig.GetComponent<AgentCar>());


    }

    override public void UpdateEnvironmentMaster()
    {
        dummyCar.UpdateController();
    }
}
