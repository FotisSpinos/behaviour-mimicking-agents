using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBoxAction : MonoBehaviour, EnvironmentAction
{
    // The prefub used to instanciate chasing boxe objects
    private GameObject physicsBoxPrefub;

    // A reference to the target's rig
    private Rigidbody targetRig;

    // A list of boxes currently chasing the target
    private LinkedList<ChasingBox> chasingBoxes;    

    public ThrowBoxAction(GameObject physicsBoxPrefub, Rigidbody targetRig)
    {
        // set variables
        this.physicsBoxPrefub = physicsBoxPrefub;
        this.targetRig = targetRig;

        // make sure that the GO is physics based
        if (!physicsBoxPrefub.GetComponent<Rigidbody>())
            physicsBoxPrefub.AddComponent<Rigidbody>();

        // Init linked list
        chasingBoxes = new LinkedList<ChasingBox>();
    }

    public void ExcecuteAction()
    {
        // Spawn ThrowBox
        GameObject throwBoxGo = GameObject.Instantiate(physicsBoxPrefub, new Vector3(0.0f, 10.0f, 0.0f), Quaternion.identity);

        ChasingBox throwBox = throwBoxGo.AddComponent<ChasingBox>();
        throwBox.init(10.0f, new Vector3(0.0f, 10.0f, 0.0f), targetRig);

        // add element to the linked list
        chasingBoxes.AddLast(throwBox);
    }

    public void InitAction()
    {

    }

    public void UpdateAction()
    {

    }
}
