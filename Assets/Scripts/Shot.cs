using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField]
    private float _lifetime;
    [SerializeField]
    private float _speed;

    private Rigidbody _rb;

    void Awake()
    {
        Destroy(gameObject, _lifetime);
        _rb = GetComponent<Rigidbody>();

        Fire();
    }

    void Fire()
    {
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
        Debug.Log("position: " + transform.position);
    }

    void Update()
    {
        // _rb.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coliiding ... ");
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy)
        {
            Debug.Log("... with enemy");
            enemy.TakeDamage(enemy.GetMaxHealth());
            enemy.Die();
        }

        ReflectingObject reflectingObject = other.gameObject.GetComponent<ReflectingObject>();

        if (reflectingObject)
        {
            Debug.Log(" ... with reflectingObject");
            if (reflectingObject.reflectionType == ReflectingObject.ReflectionType.full)
            {

            }
        }
    }
}
