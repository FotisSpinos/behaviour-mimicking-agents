using UnityEngine;

public class InvisibleForceAction : EnvironmentAction
{
    private float torqueMagnitude;
    private Rigidbody rig;

    public InvisibleForceAction(float torqueMagnitude, Rigidbody rig)
    {
        this.torqueMagnitude = torqueMagnitude;
        this.rig = rig;
    }

    public void ExecuteAction()
    {
        // define random direction
        Vector3 dir = new Vector3(0.0f, Random.Range(0.0f, 360), 0.0f);
        dir.Normalize();

        // apply force to rigidbody
        rig.AddTorque(dir * torqueMagnitude);
    }

    public void InitAction()
    {
        
    }

    public void UpdateAction()
    {
    
    }
}