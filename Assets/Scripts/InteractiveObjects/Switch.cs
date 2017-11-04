using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private List<SwitchTarget> _affectedObjects;

    void Activate()
    {
        InitializeSwitch();
        ExecuteSwitch();
        FinalizeSwitch();
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Activate();
            }
        }
    }

    void InitializeSwitch()
    {
        // animations, sounds, etc
        SoundManager.instance.Play(SoundManager.AudioClips.Switch);
    }

    void ExecuteSwitch()
    {
        foreach (SwitchTarget target in _affectedObjects)
        {
            StartCoroutine(target.MoveAffectedObject());
        }
    }

    void FinalizeSwitch()
    {
        // animation, sounds, etc
    }
}
