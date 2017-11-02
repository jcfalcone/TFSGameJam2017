using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStateManager
{
    [SerializeField]
    List<TemplateState> states = new List<TemplateState>();

    Enemy parent;

	// Use this for initialization
    void Init (Enemy _parent) 
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
