using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TemplateState
{

    [Header("Movement")]
    [SerializeField]
    [Range(0f, 500f)]
    protected float speed;

    [Header("Reference")]
    [SerializeField]
    protected GameObject model;

    [SerializeField]
    protected Animator modelAnimator;

    [SerializeField]
    protected SoundManager.AudioClips clip = SoundManager.AudioClips.Invalid;

    virtual public void Init()
    {
        if (this.model)
        {
            this.model.SetActive(false);
        }
    }

    virtual public void Start()
    {
        SoundManager.instance.Play(clip);

        if (this.model)
        {
            this.model.SetActive(true);
        }
    }

    virtual public void End()
    {
        if (this.model)
        {
            this.model.SetActive(false);
        }
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
