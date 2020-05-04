using UnityEngine;

internal class TeleportAction : EnvironmentAction
{
    private AgentCarController agentCar;
    private float posRange;
    private float rotRange;

    public TeleportAction(AgentCarController agentCar, float posRange, float rotRange)
    {
        this.agentCar = agentCar;
        this.posRange = posRange;
        this.rotRange = rotRange;
    }

    public void ExecuteAction()
    {
        agentCar.transform.localPosition += GetRandomPosition(posRange);

        agentCar.transform.localEulerAngles += GetRandomEulerAngels(rotRange);
    }

    public Vector3 GetRandomPosition(float range)
    {
        // define a random direction
        Vector3 dir = new Vector3(Random.Range(0, 360), 0.0f, Random.Range(0, 360));
        dir.Normalize();

        // define a random range
        float randomRange = Random.Range(0, range);

        // return resulting vector
        return dir * randomRange;
    }

    public Vector3 GetRandomEulerAngels(float range)
    {
        // define a random direction
        Vector3 dir = new Vector3(0.0f, Random.Range(0, 360), 0.0f);
        dir.Normalize();

        // define a random range
        float randomRange = Random.Range(0, range);

        // return resulting vector
        return dir * randomRange;
    }

    public void InitAction()
    {
        
    }

    public void UpdateAction()
    {
        
    }
}