using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface VehicleController
{
    void UpdateController();
    void InitController(Vehicle vehicle);

    void ResetVehicleController();
}
