using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTest : Enemy 
{
    [SerializeField]
    EnemyStateManager stateManager;


	// Use this for initialization
	void Start () 
    {
        this.stateManager.Init(this);
        this.stateManager.SetState(EnemyStateTemplate.StatesAI.Idle);
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.stateManager.Tick();	
	}
}
