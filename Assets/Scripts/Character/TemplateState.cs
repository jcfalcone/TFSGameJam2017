using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TemplateState
{

    public enum StatesAI
    {
        Invalid = -1,
        Idle = 0,
        Patrol,
        Engage,
        Dead
    }

    [Header("Movement")]
    [SerializeField]
    [Range(100f, 500f)]
    protected float speed;

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
}
