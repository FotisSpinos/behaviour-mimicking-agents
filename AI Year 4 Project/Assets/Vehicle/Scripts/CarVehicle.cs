using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CarVehicle : AbstractVehicle  
{
    private Rigidbody rb;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxAngularSpeed;

    [SerializeField] private float forwardForce;
    [SerializeField] private float steerForce;

    private bool isColliding;
    public CarVehicle()
    {

    }

    /// <summary>
    /// Initialization
    /// </summary>
    private void Start()
    {
         rb = gameObject.GetComponent<Rigidbody>();
    }

    public void OnImpactRecorded()
    {
        isColliding = false;
    }


    /// <summary>
    /// Use this method to pass input to the vehicle
    /// </summary>
    /// <param name="steeringAmount"> the amount the vehicle steers. When the variable is less than 0.5 the agent will steer to the left</param>
    /// <param name="speed"></param>
    public override void SetVehicleInput(params float[] parameters)
    {
        if(parameters == null || parameters.Length != 2)
        {
            Debug.LogWarning("Parameter number is incorect");
            return;
        }

        if (rb.angularVelocity.magnitude < maxAngularSpeed &&
            rb.velocity.magnitude > 0.4f)
            Steer(parameters[0]);

        if(rb.velocity.magnitude < maxSpeed)
            Move(parameters[1]);
    }

    /// <summary>
    /// Steers object by applying a torque force on the Y axis
    /// Steering force = steeringAmount * steerForce
    /// </summary>
    /// <param name="steeringAmount">The amount of torque </param>
    public void Steer(float steeringAmount)
    {
        if(steeringAmount != 0.0f)
            rb.AddTorque(0, steeringAmount * steerForce, 0);
    }

    /// <summary>
    /// Moves the object by applying a force at the "up" direction
    /// steering
    /// </summary>
    /// <param name="forwardAmount">The amount of force applied to the object</param>
    public void Move(float forwardAmount)
    {
        if (forwardAmount != 0.0f)
            rb.AddForce(transform.up * forwardAmount * forwardForce);
    }

    // Getters

    public override Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    public Vector3 GetRotation()
    {
        return transform.rotation.eulerAngles;
    }

    public override Vector3 GetPosition()
    {
        return transform.position;
    }

    public override float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public override GameObject GetGameObject()
    {
        return gameObject;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Chasing box")
            isColliding = true;
    }

    public override bool IsColliding()
    {
        return isColliding;
    }

    public override void ImpactRecorded()
    {
        isColliding = false;
    }

    public override void SetVeolcity(Vector3 velocity)
    {
        throw new System.NotImplementedException();
    }
}
