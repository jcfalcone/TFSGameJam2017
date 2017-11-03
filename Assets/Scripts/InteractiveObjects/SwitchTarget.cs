using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTarget : MonoBehaviour {

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Transform _destination;

	[SerializeField]
	private float _distanceTolerance;

	public IEnumerator MoveAffectedObject()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
        { 
            while (Vector3.Distance(transform.position, _destination.position) > _distanceTolerance)
            {
                Vector3 direction = (_destination.position - transform.position).normalized;
				rb.MovePosition(transform.position + direction * _speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        yield return null;
    }
}
