using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private Transform _affectedObject;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Transform _destination;

    void Activate()
    {
        InitializeSwitch();
        ExecuteSwitch();
        FinalizeSwitch();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Activate();
        }
    }

    void InitializeSwitch()
    {
        // animations, sounds, etc
    }

    void ExecuteSwitch()
    {
        StartCoroutine(MoveAffectedObject());
    }

    void FinalizeSwitch()
    {
        // animation, sounds, etc
    }

    IEnumerator MoveAffectedObject()
    {
        Rigidbody affectedObjectedRB = _affectedObject.GetComponent<Rigidbody>();
        if (affectedObjectedRB)
        {
            Vector3 direction = (_destination.position - _affectedObject.position).normalized;
            while (Vector3.Distance(_affectedObject.position, _destination.position) > 1)
            {
                affectedObjectedRB.MovePosition(_affectedObject.position + direction * _speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        yield return null;
    }
}
