/*
 * Provides the functionality followed by chasing boxes
 * Provide the means of changing the agent's state
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBox : MonoBehaviour
{
    // A reference to the object's rigidbody
    private Rigidbody throwBoxRig;

    // The target chased by the object
    private Rigidbody targetRig;

    // The force magnitude applied to the game objects rigid body
    private float throwForce = 50.0f;

    // Represents the time before the object is destroyed
    private float cleanUpTimer;

    // When true the object has to be destroyed
    private bool destroy;

    /// <summary>
    /// Initializer spawning a box at a given location
    /// </summary>
    /// <param name="throwBoxPrefub">The prefub used to instanciate game objects in real time</param>
    /// <param name="cleanUpTimer">The amount of time before the object is deleted</param>
    /// <param name="position">The position used to spawn the game object</param>
    public void init(float cleanUpTimer, Vector3 position, Rigidbody target)
    {
        this.cleanUpTimer = cleanUpTimer;
        this.targetRig = target;

        Create(position);
    }

    // Sets the force applied to the object on each update
    public void SetThrowForce(float throwForce)
    {
        this.throwForce = throwForce;
    }

    // Used to instanciate a chasing box game object
    public void Create(Vector3 position)
    {
        gameObject.name = "Chasing box";

        throwBoxRig = gameObject.GetComponent<Rigidbody>();

        if (throwBoxRig == null)
            throwBoxRig = gameObject.AddComponent<Rigidbody>();

    }

    // Update is called once per frame
    public void Update()
    {
        cleanUpTimer -= Time.deltaTime;

        throwBoxRig.AddForce(((targetRig.position + targetRig.velocity * Vector3.Distance(targetRig.position, gameObject.transform.position) * Time.deltaTime) - 
            gameObject.transform.position).normalized * throwForce);

        if (cleanUpTimer <= 0)
            destroy = true;

        if (destroy)
            DestroyImmediate(this.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == targetRig.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}
