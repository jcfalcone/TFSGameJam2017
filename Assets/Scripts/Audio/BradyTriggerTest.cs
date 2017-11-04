using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BradyTriggerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll)
    {
        SoundManager.instance.Play(SoundManager.AudioClips.GameMusic1);
    }
}
