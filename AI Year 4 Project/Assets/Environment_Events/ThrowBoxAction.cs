using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBoxAction : MonoBehaviour, EnvironmentAction
{
    private GameObject physicsBoxPrefub;
    private Rigidbody targetRig;

    private LinkedList<ChasingBox> chasingBoxes;

    public ThrowBoxAction(GameObject physicsBoxPrefub, Rigidbody targetRig)
    {
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
        ChasingBox throwBox = new ChasingBox(physicsBoxPrefub, 10.0f, new Vector3(0.0f, 10.0f, 0.0f), targetRig);

        // add element to the linked list
        chasingBoxes.AddLast(throwBox);
    }

    public void InitAction()
    {

    }

    public void UpdateAction()
    {
        LinkedListNode<ChasingBox> current = chasingBoxes.First;

        //Iterate through the elements of the linked list
        while(current != null)
        {
            if(current.Value.DestroyRequired()) // Destroy GO and ChasingBox
            {
                GameObject boxGoRef = current.Value.GetThrowBox();
                chasingBoxes.RemoveFirst();
                DestroyImmediate(boxGoRef, true);
            }
            else    //Update ChasingBox
            {
                current.Value.UpdateThrowBox();
            }

            current = current.Next;
        }
    }
}
