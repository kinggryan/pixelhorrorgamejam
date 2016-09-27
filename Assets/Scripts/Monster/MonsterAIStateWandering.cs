using UnityEngine;
using System.Collections;

public class MonsterAIStateWandering : MonsterAIState
{
    public float startSearchingMinRelativeVolume = 1;

    override public void Start()
    {
        base.Start();

        // Find a random position nearby on the Nav Mesh and move towards that.
        agent.SetDestination(RandomWanderPoint());
    }

    Vector3 RandomWanderPoint()
    {
        float wanderDistance = 10;

        Vector2 wanderDirection = Random.insideUnitCircle.normalized;
        Vector3 wanderPoint = transform.position + wanderDistance * new Vector3(wanderDirection.x, 0, wanderDirection.y);
        NavMeshHit hit;
        while(!NavMesh.SamplePosition(wanderPoint,out hit,10,NavMesh.AllAreas))
        {
            wanderDirection = Random.insideUnitCircle.normalized;
            wanderPoint = transform.position + wanderDistance * new Vector3(wanderDirection.x, 0, wanderDirection.y);
        }

        return hit.position;
    }

    public override MonsterAIState ReachedTargetPosition()
    {
        // Find a random position nearby on the Nav Mesh and move towards that.
        agent.SetDestination(RandomWanderPoint());

        return null;
    }

    public override MonsterAIState HearSound(Vector3 soundPoint, float soundVolume)
    {
        if(soundVolume > startSearchingMinRelativeVolume)
        {
            MonsterAIStateSearching searchingState = (MonsterAIStateSearching)gameObject.AddComponent<MonsterAIStateSearching>();
            searchingState.minChangeTargetVolume = soundVolume;
            searchingState.targetPosition = soundPoint;
            Object.Destroy(this);
            return searchingState;
        }
        else
        {
            return null;
        }
    }
}

