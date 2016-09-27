using UnityEngine;
using System.Collections;

public class MonsterAIStateSearching : MonsterAIState
{
    public float minChangeTargetVolume;

    public MonsterAIStateSearching(Vector3 soundPoint, float relativeSound)
    {
        this.minChangeTargetVolume = relativeSound;
        this.targetPosition = soundPoint;
    }

    override public void Start()
    {
        base.Start();

        // Move Towards the target Sound
        agent.SetDestination(targetPosition);
    }

    override public MonsterAIState HearSound(Vector3 soundPoint, float soundVolume)
    {
        // If the sound is loud enough, target it
        if (soundVolume > minChangeTargetVolume)
        {
            targetPosition = soundPoint;
            minChangeTargetVolume = soundVolume;
        }

        // Don't Change on hear sound
        return null;
    }

    public override MonsterAIState ReachedTargetPosition()
    {
        MonsterAIStateResting state = gameObject.AddComponent<MonsterAIStateResting>();
        state.restTime = 1.5f;
        Object.Destroy(this);
        return state;
    }
}
