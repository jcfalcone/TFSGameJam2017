using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : EnemyStateTemplate 
{
    float timeTotal;

    public DieState()
    {
        this.state = StatesAI.Die;
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
        if (this.timeTotal > this.timeInState)
        {
            return StatesAI.Idle;
        }

        return base.NextState();
    }
}
