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

    [SerializeField]
    private float _rotationSpeed;

    void Awake()
    {
        Destroy(gameObject, _lifetime);
        _rb = GetComponent<Rigidbody>();

        Fire();
    }

    void Fire()
    {
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    void Update()
    {
        // _rb.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
        // _rb.velocity = transform.forward * _speed * Time.deltaTime;
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ShotReflection"))
        {
            if (other.transform.rotation.x != 0)
            {
                Debug.DrawRay(transform.position, other.contacts[0].normal * 10, Color.red, _lifetime);
                Ricochet(other);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }

    void Ricochet(Collision collisionObject)
    {

        Vector3 ricochet = Vector3.zero;

        // ------ OPTION A1 -------
        // ricochet = collisionObject.contacts[0].normal;

        // ------ OPTION A2 -------
        // ricochet = Vector3.Reflect(transform.forward, collisionObject.contacts[0].normal);

        // ------ OPTION A3 -------
        // Ray ray = new Ray(transform.position, transform.forward);
        // RaycastHit hit;

        // Physics.Raycast(ray, out hit, Time.deltaTime * _speed + 0.1f);
        // ricochet = Vector3.Reflect(ray.direction, hit.normal);

        // float rotation = 90 - Mathf.Atan2(ricochet.z, ricochet.x) * Mathf.Rad2Deg;
        // ricochet = new Vector3(0, rotation, 0);

        // ------ OPTION A4 -------
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Time.deltaTime * _speed + 1);

        Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);
        ricochet = localForward.normalized + hit.normal;

        // ------ OPTION A5 -------
        // ricochet = transform.up;

        // ------ OPTION 5 -------
        // Vector3 ricochet = Vector3.Cross(collisionObject.contacts[0].point, transform.position);
        // Debug.DrawRay(transform.position, ricochet * 10, Color.green, _lifetime);
        // transform.eulerAngles = ricochet;

        Debug.DrawRay(transform.position, ricochet * 10, Color.green, _lifetime);

        Debug.Log("ricochet: " + ricochet);

        // ------ OPTION B1 -------
        // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(ricochet), 0.00001f);

        // ------ OPTION B2 -------
        // transform.eulerAngles = ricochet;

        // ------ OPTION B3 -------
        // transform.rotation = Quaternion.Euler(ricochet);

        // ------ OPTION B4 -------
        // transform.Rotate(ricochet);

        // ------ OPTION B5 -------
        // Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(ricochet), _rotationSpeed);

        // ------ OPTION B6 -------
        // transform.LookAt(ricochet);

        // ------ OPTION B7 -------
        // Quaternion newRotation = Quaternion.LookRotation(ricochet);
        // Quaternion.Slerp(transform.rotation, newRotation, 0.01f);

        // ------ OPTION B8 -------
        // transform.localRotation = Quaternion.Euler(ricochet);

        // ------ OPTION B9 -------
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, ricochet, _rotationSpeed, 0));



        // Debug.Break();
    }
}