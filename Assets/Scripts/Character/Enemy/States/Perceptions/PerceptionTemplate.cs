using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionTemplate
{
    protected Enemy parent;

    public virtual void Init(Enemy _parent)
    {
        this.parent = _parent;
    }

    public virtual EnemyStateTemplate.StatesAI Run()
    {
        return EnemyStateTemplate.StatesAI.Invalid;
    }
}
