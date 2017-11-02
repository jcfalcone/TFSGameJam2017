using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntangibleState : TemplateState 
{
    public IntangibleState()
    {
        this.state = States.Intangible;
    }

    override public void Tick()
    {
    }

    override public void FixedTick()
    {
    }
}
