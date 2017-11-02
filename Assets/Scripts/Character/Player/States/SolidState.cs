using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidState : PlayerStateTemplate 
{
    public SolidState()
    {
        this.state = States.Solid;
    }

    override public void Tick()
    {
    }

    override public void FixedTick()
    {
    }
}
