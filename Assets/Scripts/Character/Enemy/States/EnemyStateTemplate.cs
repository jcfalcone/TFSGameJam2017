using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStateTemplate : TemplateState 
{

    public enum StatesAI
    {
        Invalid = -1,
        Idle = 0,
        Patrol,
        Engage,
        Die
    }

    [Header("Movement Enemy")]
    [SerializeField]
    [Range(0f, 30f)]
    protected float rotationSpeed;

    [SerializeField]
    [Range(0f, 3f)]
    protected float pathProximityTolerance;


    [Header("State")]
    [SerializeField]
    protected StatesAI state;


    [SerializeField]
    [Range(-1f, 10f)]
    protected float timeInState;

    protected List<PerceptionTemplate> perceptions = new List<PerceptionTemplate>();

    protected Enemy parent;

    public override void Start()
    {
    }

    public override void End()
    {
    }

    public virtual StatesAI NextState()
    {
        return StatesAI.Invalid;
    }

    public virtual StatesAI Perceptions()
    {
        for (int count = 0; count < this.perceptions.Count; count++)
        {
            EnemyStateTemplate.StatesAI state = this.perceptions[count].Run();

            if (state != StatesAI.Invalid)
            {
                return state;
            }
        }

        return StatesAI.Invalid;
    }

    public void Init(Enemy _parent)
    {
        this.parent = _parent;

        for (int count = 0; count < this.perceptions.Count; count++)
        {
            this.perceptions[count].Init(this.parent);
        }
    }
}
