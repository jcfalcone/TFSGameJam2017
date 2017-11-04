using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : PlayerStateTemplate 
{
    public DeadState()
    {
        this.state = States.Dead;
    }

    public override void Start()
    {
        base.Start();
        LevelManager.instance.ResetLevel();
    }

    override public void Tick()
    {
    }

    override public void FixedTick()
    {
    }
}
