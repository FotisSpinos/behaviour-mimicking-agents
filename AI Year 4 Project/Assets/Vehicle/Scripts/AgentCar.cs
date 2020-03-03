using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class AgentCar : MonoBehaviour, Vehicle
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxAngularSpeed;

    [SerializeField] private float forwardForce;
    [SerializeField] private float steerForce;

    /// <summary>
    /// Initialization
    /// </summary>
    private void Start()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Use this method to pass input to the vehicle
    /// </summary>
    /// <param name="steeringAmount"> the amount the vehicle steers. When the variable is less than 0.5 the agent will steer to the left</param>
    /// <param name="speed"></param>
    public void SetVehicleInput(float steeringAmount, float speed)
    {
        if (rb.angularVelocity.magnitude < maxAngularSpeed &&
            rb.velocity.magnitude > 0.7f)
            Steer(steeringAmount);

        if(rb.velocity.magnitude < maxSpeed)
            Move(speed);
    }

    /// <summary>
    /// Steers object by applying a torque force on the Y axis
    /// Steering force = steeringAmount * steerForce
    /// </summary>
    /// <param name="steeringAmount">The amount of torque </param>
    public void Steer(float steeringAmount)
    {
        rb.AddTorque(0, steeringAmount * steerForce, 0);
    }

    /// <summary>
    /// Moves the object by applying a force at the "up" direction
    /// steering
    /// </summary>
    /// <param name="forwardAmount">The amount of force applied to the object</param>
    public void Move(float forwardAmount)
    {
        rb.AddForce(transform.up * forwardAmount * forwardForce);
    }

    // Getters

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    public Vector3 GetRotation()
    {
        return transform.rotation.eulerAngles;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
