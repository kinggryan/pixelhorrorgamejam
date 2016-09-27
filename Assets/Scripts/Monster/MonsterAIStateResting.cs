using UnityEngine;
using System.Collections;

public class MonsterAIStateResting : MonsterAIState {

    public float restTime;

    override public void Start()
    {
        base.Start();
        targetPosition = transform.position;
    }

    override public void Update()
    {
        restTime -= Time.deltaTime;
    }

    public override MonsterAIState ReachedTargetPosition()
    {
        if (restTime <= 0)
        {
            MonsterAIState state = gameObject.AddComponent<MonsterAIStateWandering>();
            Object.Destroy(this);
            return state;
        }

        return null;
    }
}
