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

        Debug.Log("... with enemy");
        // enemy.TakeDamage(enemy.GetMaxHealth());
        //enemy.Die();

        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);

    }

    void Update()
    {
        _rb.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
        // _rb.velocity = transform.forward * _speed * Time.deltaTime;
        Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);
    }

    void OnCollisionEnter(Collision other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy)
        {

            enemy.TakeDamage(enemy.GetMaxHealth());
            // enemy.Die();
        }

        ReflectingObject reflectingObject = other.gameObject.GetComponent<ReflectingObject>();

        if (reflectingObject)
        {
            Ricochet(other, reflectingObject);
        }
    }

    void Ricochet(Collision collisionObject, ReflectingObject reflectingObject)
    {
        // ------ OPTION 0 -------
        // Vector3 reflected = Vector3.zero;

        // if (reflectingObject.reflectionType == ReflectingObject.ReflectionType.full)
        // {
        //     Ray ray = new Ray(transform.position, transform.forward);
        //     RaycastHit hit;

        //     Physics.Raycast(ray, out hit, Time.deltaTime * _speed + 1);
        //     reflected = Vector3.Reflect(ray.direction, hit.normal);
        // }
        // else if (reflectingObject.reflectionType == ReflectingObject.ReflectionType.rightAngle)
        // {
        //     reflected = Vector3.Reflect(transform.forward, collisionObject.contacts[0].normal);
        // }

        // float rotation = 90 - Mathf.Atan2(reflected.z, reflected.x) * Mathf.Rad2Deg;
        // transform.eulerAngles = new Vector3(0, rotation, 0);

        // ------ OPTION 1 -------
        // Vector3 reflected = transform.forward.normalized + collisionObject.contacts[0].normal;
        // transform.eulerAngles = reflected;

        // ------ OPTION 2 -------
        // Ray ray = new Ray(transform.position, transform.forward);
        // RaycastHit hit;
        // Physics.Raycast(ray, out hit, Time.deltaTime * _speed + 1);

        // Vector3 reflected = transform.forward.normalized + hit.normal;
        // transform.eulerAngles = reflected;
        // Debug.DrawLine(hit.point, hit.normal, Color.red);

        // ------ OPTION 3 -------
        // Ray ray = new Ray(transform.position, transform.forward);
        // RaycastHit hit;
        // Physics.Raycast(ray, out hit, Time.deltaTime * _speed + 1);

        // Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);
        // Vector3 reflected = localForward.normalized + hit.normal;
        // transform.eulerAngles = reflected;
        // Debug.DrawLine(hit.point, hit.normal, Color.red);

        // ------ OPTION 4 -------
        // transform.eulerAngles = transform.up;

        // ------ OPTION 5 -------
        // Vector3 ricochet = Vector3.Cross(transform.position, collisionObject.contacts[0].point);
        // transform.eulerAngles = ricochet;
    }
}