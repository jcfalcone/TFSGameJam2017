using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candles : MonoBehaviour
{
    [SerializeField] GameObject fire;
    //[SerializeField] LevelManager levelManager;

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.name == "Lantern")
        {
            fire.SetActive(true);
            SoundManager.instance.Play(SoundManager.AudioClips.MatchStrike);
            Destroy(this);
        }
    }

    void OnTriggerStay(Collider c)
    {
        if(Input.GetKey(KeyCode.E))
        {
            // Call Level Manager to add 1 to objectives
            fire.SetActive(true);
            SoundManager.instance.Play(SoundManager.AudioClips.MatchStrike);
            Destroy(this);
        }
    }
}
