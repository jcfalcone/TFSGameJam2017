using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LEVER_STATES
{
    OFFLINE,
    ONLINE,
    Size
}

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject lever;
    [SerializeField] List<Material> leverMaterials;

    [SerializeField] List<SwitchTarget> _affectedObjects;

    void Activate()
    {
        InitializeSwitch();
        ExecuteSwitch();
        FinalizeSwitch();
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.CompareTag("PlayerCollider"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                lever.GetComponent<Renderer>().material = leverMaterials[(int)LEVER_STATES.ONLINE];
                transform.rotation = Quaternion.Euler(180.0f, 90.0f, 0.0f);
                GetComponent<BoxCollider>().enabled = false;
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
            target.Activate();
            //StartCoroutine(target.MoveAffectedObject());
        }
    }

    void FinalizeSwitch()
    {
        // animation, sounds, etc
    }
}
