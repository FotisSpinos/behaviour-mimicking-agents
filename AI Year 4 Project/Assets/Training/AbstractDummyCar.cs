using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDummyCarController : VehicleController
{
    protected int animIndex;
    protected int stateIndex;

    protected bool enableRandomAnimIndex;

    public bool EnableRandomAnimIndex
    {
        set
        {
            enableRandomAnimIndex = value;
        }
    }


    public delegate void Reset();
    public event Reset ResetOccuredEvent;


    public AbstractDummyCarController()
    {
        enableRandomAnimIndex = true;
    }

    public void OnResetOccured()
    {
        ResetOccuredEvent?.Invoke();
    }

    public abstract void InitController(AbstractVehicle vehicle);

    public abstract void UpdateController();

    public abstract void ResetVehicleController();

    public abstract AbstractVehicle GetVehicle();

    public abstract Vector3 GetCurrentVelocity();
}
