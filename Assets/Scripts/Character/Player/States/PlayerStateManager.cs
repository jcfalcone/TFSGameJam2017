using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateManager : StateManagerTemplate
{

    [SerializeField]
    protected List<PlayerStateTemplate> states = new List<PlayerStateTemplate>();

    [SerializeField]
    PlayerStateTemplate.States currState;

    [SerializeField]
    PlayerStateTemplate.States desireState;

    Player parent;

    public PlayerStateManager()
    {
        this.states.Add(new DefaultState());
        this.states.Add(new IntangibleState());
        this.states.Add(new SolidState());
        this.states.Add(new DeadState());
    }

    public void test()
    {
        this.states.Add(new DeadState());
    }

    public void Init(Player _parent)
    {
        this.parent = _parent;

        this.currState = PlayerStateTemplate.States.Invalid;
        this.desireState = PlayerStateTemplate.States.Invalid;

        for (int count = 0; count < this.states.Count; count++)
        {
            this.states[count].Init();
        }
    }

    override public void Start()
    {
    }

    override public void Tick()
    {
        this.CheckState();

        if(this.currState != PlayerStateTemplate.States.Invalid)
        {
            this.states[(int)this.currState].Tick();
        }

        this.parent.RecoverStamina();
    }

    override public void FixedTick()
    {
        this.CheckState();

        if(this.currState != PlayerStateTemplate.States.Invalid)
        {
            this.states[(int)this.currState].FixedTick();
        }
    }

    void CheckState()
    {
        if (this.desireState == PlayerStateTemplate.States.Invalid)
        {
            return;
        }

        if (this.desireState == this.currState)
        {
            return;
        }

        if (this.currState != PlayerStateTemplate.States.Invalid)
        {
            this.states[(int)this.currState].End();
        }

        this.states[(int)this.desireState].Start();

        this.currState = this.desireState;
        this.desireState = PlayerStateTemplate.States.Invalid;

        this.parent.SetIsMovable(this.isMovable());
        this.parent.SetupAnimator(this.GetAnimator());
        this.parent.SetStaminaRecoverRate(this.GetStaminaRecover());
        this.parent.SetStaminaBurnRate(this.GetStaminaBurn());
        this.parent.SetMovePseed(this.GetSpeed());
        this.parent.SetLanternRender(this.GetLanternRender());
        this.parent.PlayAura();
    }

    public void SetState(PlayerStateTemplate.States _State)
    {
        if (this.currState == _State)
        {
            return;
        }

        this.desireState = _State;
    }

    public float GetSpeed()
    {
        if (this.currState == PlayerStateTemplate.States.Invalid)
        {
            return -1f;
        }

        return this.states[(int)this.currState].GetSpeed();
    }

    public Animator GetAnimator()
    {
        if (this.currState == PlayerStateTemplate.States.Invalid)
        {
            return null;
        }

        return this.states[(int)this.currState].GetAnimator();
    }

    public bool isMovable()
    {
        if (this.currState == PlayerStateTemplate.States.Invalid)
        {
            return false;
        }

        return this.states[(int)this.currState].IsMovable();
    }

    public float GetStaminaRecover()
    {
        if (this.currState == PlayerStateTemplate.States.Invalid)
        {
            return -1f;
        }

        return this.states[(int)this.currState].GetStaminaRecover();
    }

    public float GetStaminaBurn()
    {
        if (this.currState == PlayerStateTemplate.States.Invalid)
        {
            return -1f;
        }

        return this.states[(int)this.currState].GetStaminaBurn();
    }

    public Renderer GetLanternRender()
    {
        if (this.currState == PlayerStateTemplate.States.Invalid)
        {
            return null;
        }

        return this.states[(int)this.currState].GetLanternRender();
    }

    public PlayerStateTemplate.States GetState()
    {
        return this.currState;
    }
}
