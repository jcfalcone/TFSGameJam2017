using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class StateManagerTemplate
{
    public int banana = 10;

    [SerializeField]
    protected List<TemplateState> states = new List<TemplateState>();

    public StateManagerTemplate()
    {
    }

    abstract public void Start();
    abstract public void Tick();
    abstract public void FixedTick();
}
