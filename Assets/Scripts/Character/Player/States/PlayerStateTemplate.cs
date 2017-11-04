using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateTemplate : TemplateState 
{

    public enum States
    {
        Invalid = -1,
        Default = 0,
        Intangible,
        Solid,
        Dead
    }


    [Header("Player")]
    [SerializeField]
    protected States state;

    [Header("Reference")]
    [SerializeField]
    protected Renderer lanternRender;

    [Header("Movement")]
    [SerializeField]
    bool isMovable = true;

    [SerializeField]
    [Range(0f, 200f)]
    protected float staminaRecoverRate;

    [SerializeField]
    [Range(0f, 200f)]
    protected float staminaBurnRate;

    virtual public bool IsMovable()
    {
        return this.isMovable;
    }

    virtual public float GetStaminaRecover()
    {
        return this.staminaRecoverRate;
    }

    virtual public float GetStaminaBurn()
    {
        return this.staminaBurnRate;
    }

    virtual public Renderer GetLanternRender()
    {
        return this.lanternRender;
    }
}
