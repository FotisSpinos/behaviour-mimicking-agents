using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleTest : MonoBehaviour
{
    [SerializeField] private Vehicle vehicle;

    private void Update()
    {
        vehicle.SetVehicleInput(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
    }
}
