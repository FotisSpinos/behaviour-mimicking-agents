/*
 * Provides the functionality followed by chasing boxes
 * Provide the means of changing the agent's state
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBox
{
    // The game object displayed on the scene. The class provides functionality to handled this object's behaviour
    private GameObject throwBox;

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
    /// Constructor spawning a box at a given location
    /// </summary>
    /// <param name="throwBoxPrefub">The prefub used to instanciate game objects in real time</param>
    /// <param name="cleanUpTimer">The amount of time before the object is deleted</param>
    /// <param name="position">The position used to spawn the game object</param>
    public ChasingBox(GameObject throwBoxPrefub, float cleanUpTimer, Vector3 position, Rigidbody target)
    {
        this.cleanUpTimer = cleanUpTimer;
        this.targetRig = target;

        Create(position, throwBoxPrefub);
    }

    // Sets the force applied to the object on each update
    public void SetThrowForce(float throwForce)
    {
        this.throwForce = throwForce;
    }

    // Used to instanciate a chasing box game object
    public void Create(Vector3 position, GameObject prefub)
    {
        throwBox = GameObject.Instantiate(prefub, position, Quaternion.identity);
        throwBox.name = "Chasing box";

        throwBoxRig = throwBox.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void UpdateThrowBox()
    {
        cleanUpTimer -= Time.deltaTime;

        throwBoxRig.AddForce((targetRig.position - throwBox.transform.position).normalized * throwForce);
    }

    // Returns true if the object has to be destroyed
    public bool DestroyRequired()
    {
        if (cleanUpTimer <= 0)
            return true;

       return false;
    }

    // Returns a reference of the instanciated game object
    public GameObject GetThrowBox()
    {
        return this.throwBox;
    }
}
