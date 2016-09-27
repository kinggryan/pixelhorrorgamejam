using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class MonsterAIState : MonoBehaviour
{
    public Vector3 targetPosition;
    protected NavMeshAgent agent;

    virtual public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    virtual public void Update()
    {
        if (Vector3.Distance(agent.destination, transform.position + Vector3.down) < 0.5f)
        {
            this.ReachedTargetPosition();
        }
    }
    
    virtual public MonsterAIState ReachedTargetPosition()
    {
        return null;
    }

    virtual public MonsterAIState HearSound(Vector3 soundPoint, float soundVolume)
    {
        return null;
    }
}
