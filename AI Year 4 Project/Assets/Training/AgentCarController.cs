using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;

public class AgentCarController : Agent, VehicleController
{
    public virtual void InitController(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }

    public virtual void ResetVehicleController()
    {
        throw new NotImplementedException();
    }

    public virtual void UpdateController()
    {
        throw new NotImplementedException();
    }
}

