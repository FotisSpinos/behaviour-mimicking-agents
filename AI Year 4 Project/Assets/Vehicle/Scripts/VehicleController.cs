﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface VehicleController
{
    void UpdateController();
    void InitController(AbstractVehicle vehicle);

    void ResetVehicleController();
}
