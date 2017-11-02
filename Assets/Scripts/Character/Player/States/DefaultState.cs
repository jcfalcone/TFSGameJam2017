using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefaultState : TemplateState
{
    public DefaultState()
    {
        this.state = States.Default;
    }

    override public void Tick()
    {
    }

    override public void FixedTick()
    {
    }
}
