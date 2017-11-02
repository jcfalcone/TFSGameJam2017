using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStateManager : StateManagerTemplate
{
    [SerializeField]
    TemplateState.StatesAI currState;

    [SerializeField]
    TemplateState.StatesAI desireState;

    Enemy parent;

	// Use this for initialization
    void Init (Enemy _parent) 
    {
        this.parent = _parent;

        this.currState = TemplateState.StatesAI.Invalid;
        this.desireState = TemplateState.StatesAI.Invalid;
		
    }

    override public void Tick()
    {
        this.CheckState();

        if(this.currState != TemplateState.States.Invalid)
        {
            this.states[(int)this.currState].Tick();
        }
    }

    override public void FixedTick()
    {
        this.CheckState();

        if(this.currState != TemplateState.States.Invalid)
        {
            this.states[(int)this.currState].FixedTick();
        }
    }

    void CheckState()
    {
        if (this.desireState == TemplateState.States.Invalid)
        {
            return;
        }

        if (this.desireState == this.currState)
        {
            return;
        }

        if (this.currState != TemplateState.States.Invalid)
        {
            this.states[(int)this.currState].End();
        }

        this.states[(int)this.desireState].Start();

        this.currState = this.desireState;
        this.desireState = TemplateState.States.Invalid;
    }

    public void SetState(TemplateState.States _State)
    {
        if (this.currState == _State)
        {
            return;
        }

        this.desireState = _State;
    }
}
