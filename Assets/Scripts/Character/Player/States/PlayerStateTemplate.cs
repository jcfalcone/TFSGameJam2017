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
        Solid
    }


    [Header("Player")]
    [SerializeField]
    protected States state;


    [Header("Movement")]
    [SerializeField]
    bool isMovable = true;

    [SerializeField]
    [Range(0f, 2f)]
    protected float staminaRecoverRate;

    [SerializeField]
    [Range(0f, 2f)]
    protected float staminaBurnRate;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
