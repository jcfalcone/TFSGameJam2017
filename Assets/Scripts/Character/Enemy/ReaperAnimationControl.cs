using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperAnimationControl : MonoBehaviour {

    [SerializeField]
    Enemy parent;

    public void EndDeath()
    {
        this.parent.DeathAnimationIsOver();
    }
}
