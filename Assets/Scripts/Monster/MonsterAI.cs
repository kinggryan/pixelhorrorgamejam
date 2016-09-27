using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterAI : MonoBehaviour {

    NavMeshAgent agent;
    MonsterAIState state;

    public static MonsterAI monster;

    // Use this for initialization
    void Start ()
    {
        monster = this;
        agent = GetComponent<NavMeshAgent>();
        state = gameObject.AddComponent<MonsterAIStateWandering>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void HearSound(Vector3 soundPoint, float soundVolume)
    {
        float relativeVolume = CalculateRelativeVolume(soundPoint, soundVolume);
        MonsterAIState newState = state.HearSound(soundPoint, relativeVolume);
        if (newState != null) {
            state = newState;
        }
    }

    private float CalculateRelativeVolume(Vector3 soundPoint, float soundVolume)
    {
        NavMeshPath path = new NavMeshPath();
        if(NavMesh.CalculatePath(transform.position, soundPoint, NavMesh.AllAreas, path)) {
            var previousPoint = path.corners[0];
            float distance = 0;
            foreach(Vector3 point in path.corners)
            {
                distance += Vector3.Distance(point, previousPoint);
                previousPoint = point;
            }

            // The volume diminishes as a square of the distance
            return soundVolume / Mathf.Pow(distance,2);
        }

        return 0;
    }
}
