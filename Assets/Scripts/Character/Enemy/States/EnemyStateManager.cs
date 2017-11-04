using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStateManager : StateManagerTemplate
{

    [SerializeField]
    protected List<EnemyStateTemplate> states = new List<EnemyStateTemplate>();

    [SerializeField]
    EnemyStateTemplate.StatesAI currState;

    [SerializeField]
    EnemyStateTemplate.StatesAI desireState;

    Enemy parent;

    EnemyStateManager()
    {
        this.states.Add(new IdleState());
        this.states.Add(new PatrolState());
        this.states.Add(new EngageState());
        this.states.Add(new DieState());
    }

	// Use this for initialization
    public void Init (Enemy _parent) 
    {
        this.parent = _parent;

        this.currState = EnemyStateTemplate.StatesAI.Invalid;
        this.desireState = EnemyStateTemplate.StatesAI.Invalid;

        for (int count = 0; count < this.states.Count; count++)
        {
            this.states[count].Init(this.parent);
        }
		
    }

    override public void Start()
    {
    }

    override public void Tick()
    {
        this.CheckState();

        if(this.currState != EnemyStateTemplate.StatesAI.Invalid)
        {
            this.states[(int)this.currState].Tick();

            this.SetState(this.states[(int)this.currState].NextState());
        }
    }

    override public void FixedTick()
    {
        this.CheckState();

        if(this.currState != EnemyStateTemplate.StatesAI.Invalid)
        {
            this.states[(int)this.currState].FixedTick();
        }
    }

    void CheckState()
    {
        if (this.desireState == EnemyStateTemplate.StatesAI.Invalid)
        {
            return;
        }

        if (this.desireState == this.currState)
        {
            return;
        }

        if (this.currState != EnemyStateTemplate.StatesAI.Invalid)
        {
            this.states[(int)this.currState].End();
        }

        this.states[(int)this.desireState].Start();

        Debug.Log(this.parent.transform.name+" = "+this.currState +" - "+this.desireState);

        this.currState = this.desireState;
        this.desireState = EnemyStateTemplate.StatesAI.Invalid;
    }

    public void SetState(EnemyStateTemplate.StatesAI _State)
    {
        if (this.currState == _State)
        {
            return;
        }

        this.desireState = _State;
    }
}
