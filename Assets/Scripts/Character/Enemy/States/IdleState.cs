using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IdleState : EnemyStateTemplate 
{

    float timeTotal;

    public IdleState()
    {
        this.state = StatesAI.Idle;
        this.perceptions.Add(new PlayerViewPerception());
    }

    public override void Start()
    {
        base.Start();

        this.timeTotal = 0;
    }

    override public void Tick()
    {
        this.timeTotal += Time.deltaTime;
    }

    override public void FixedTick()
    {
    }

    public override StatesAI NextState()
    {
        if (this.parent.GetHealth() <= 0)
        {
            return StatesAI.Die;
        }

        StatesAI perceptionState = this.Perceptions();

        if (perceptionState != StatesAI.Invalid)
        {
            return perceptionState;
        }

        if (this.timeTotal > this.timeInState)
        {
            return StatesAI.Patrol;
        }

        return base.NextState();
    }
}
