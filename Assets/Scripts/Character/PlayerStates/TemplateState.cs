using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TemplateState
{
    public enum States
    {
        Invalid = -1,
        Default = 0,
        Intangible,
        Solid
    }

    [SerializeField]
    protected States state;

    [Header("Movement")]
    [SerializeField]
    bool isMovable = true;

    [SerializeField]
    [Range(100f, 500f)]
    protected float speed;

    [SerializeField]
    [Range(0f, 2f)]
    protected float staminaRecoverRate;

    [SerializeField]
    [Range(0f, 2f)]
    protected float staminaBurnRate;

    [Header("Reference")]
    [SerializeField]
    protected GameObject model;

    [SerializeField]
    protected Animator modelAnimator;

    virtual public void Init()
    {
        this.model.SetActive(false);
    }

    virtual public void Start()
    {
        this.model.SetActive(true);
    }

    virtual public void End()
    {
        this.model.SetActive(false);
    }

    virtual public void Tick()
    {
    }

    virtual public void FixedTick()
    {
    }

    virtual public Animator GetAnimator()
    {
        return this.modelAnimator;
    }

    virtual public float GetSpeed()
    {
        return this.speed;
    }

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
}
