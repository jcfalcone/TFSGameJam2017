using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candles : MonoBehaviour
{
    [SerializeField] GameObject fire;

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.CompareTag("Projectile"))
        {
            fire.SetActive(true);
            //SoundManager.instance.Play(SoundManager.AudioClips.MatchStrike);
            LevelManager.instance.LightUpPumpikin();
            Destroy(c.gameObject);
            Destroy(this);
        }
    }
}
