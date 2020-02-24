using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBox
{
    private GameObject throwBox;
    private Rigidbody throwBoxRig;
    private Rigidbody targetRig;

    private float throwForce = 50.0f;

    private float cleanUpTimer;
    bool destroy;

    /// <summary>
    /// Constructor spawning a box at a given location
    /// </summary>
    /// <param name="throwBoxPrefub"></param>
    /// <param name="cleanUpTimer"></param>
    /// <param name="position"></param>
    public ChasingBox(GameObject throwBoxPrefub, float cleanUpTimer, Vector3 position, Rigidbody target)
    {
        this.cleanUpTimer = cleanUpTimer;
        this.targetRig = target;

        Create(position, throwBoxPrefub);
    }

    public void SetThrowForce(float throwForce)
    {
        this.throwForce = throwForce;
    }

    public void Create(Vector3 position, GameObject prefub)
    {
        throwBox = GameObject.Instantiate(prefub, position, Quaternion.identity);
        throwBoxRig = throwBox.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void UpdateThrowBox()
    {
        cleanUpTimer -= Time.deltaTime;

        throwBoxRig.AddForce((targetRig.position - throwBox.transform.position).normalized * throwForce);
    }

    public bool DestroyRequired()
    {
        if (cleanUpTimer <= 0)
            return true;

       return false;
    }

    public GameObject GetThrowBox()
    {
        return this.throwBox;
    }
}
