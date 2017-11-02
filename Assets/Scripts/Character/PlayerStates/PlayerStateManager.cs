using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateManager : StateManagerTemplate
{
    [SerializeField]
    TemplateState.States currState;

    [SerializeField]
    TemplateState.States desireState;

    Player parent;

    public PlayerStateManager()
    {
        this.states.Add(new DefaultState());
        this.states.Add(new IntangibleState());
        this.states.Add(new SolidState());
    }

    public void Init(Player _parent)
    {
        this.parent = _parent;

        this.currState = TemplateState.States.Invalid;
        this.desireState = TemplateState.States.Invalid;
    }

    override public void Start()
    {
    }

    override public void Tick()
    {
        this.CheckState();

        if(this.currState != TemplateState.States.Invalid)
        {
            this.states[(int)this.currState].Tick();
        }

        this.parent.RecoverStamina();
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

        this.parent.SetIsMovable(this.isMovable());
        this.parent.SetupAnimator(this.GetAnimator());
        this.parent.SetStaminaRecoverRate(this.GetStaminaRecover());
        this.parent.SetStaminaBurnRate(this.GetStaminaBurn());
        this.parent.SetMovePseed(this.GetSpeed());
    }

    public void SetState(TemplateState.States _State)
    {
        if (this.currState == _State)
        {
            return;
        }

        Debug.Log(this.desireState + " - " + _State + " - " + (int)this.desireState);
        this.desireState = _State;
    }

    public float GetSpeed()
    {
        if (this.currState == TemplateState.States.Invalid)
        {
            return -1f;
        }

        return this.states[(int)this.currState].GetSpeed();
    }

    public Animator GetAnimator()
    {
        if (this.currState == TemplateState.States.Invalid)
        {
            return null;
        }

        return this.states[(int)this.currState].GetAnimator();
    }

    public bool isMovable()
    {
        if (this.currState == TemplateState.States.Invalid)
        {
            return false;
        }

        return this.states[(int)this.currState].IsMovable();
    }

    public float GetStaminaRecover()
    {
        if (this.currState == TemplateState.States.Invalid)
        {
            return -1f;
        }

        return this.states[(int)this.currState].GetStaminaRecover();
    }

    public float GetStaminaBurn()
    {
        if (this.currState == TemplateState.States.Invalid)
        {
            return -1f;
        }

        return this.states[(int)this.currState].GetStaminaBurn();
    }
}
