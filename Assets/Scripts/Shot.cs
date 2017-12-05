using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] float _lifetime;
    [SerializeField] [Range(0, 500)] float _speed;

    Rigidbody _rb;

    [SerializeField] float _rotationSpeed;
    [SerializeField] float _damage;
    [SerializeField] float _toleranceDistanceForIdle;
    [SerializeField] float _toleranceTimeforIdle;

    Vector3 _previousPosition;
    float _timeInPosition;

    int normal;

    void Awake()
    {
        Destroy(gameObject, _lifetime);

        normal = 1;
        _rb = GetComponent<Rigidbody>();

        _rb.velocity = transform.forward * normal * _speed;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().GetState() == PlayerStateTemplate.States.Solid)
        {
            Ricochet180Degrees();
        }

        if (other.gameObject.tag == "Enemy" || (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().GetState() != PlayerStateTemplate.States.Solid))
        {
            other.gameObject.GetComponent<BaseCharacter>().TakeDamage(_damage);
            Destroy(gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("ShotReflection"))
        {
            if (other.transform.rotation.x != 0)
            {
                Ricochet90Degrees();
            }
            else
            {
                Ricochet180Degrees();
                //Destroy(gameObject);
            }
        }
    }

    void Ricochet90Degrees()
    {
        Vector3 ricochet = Vector3.zero;
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Time.deltaTime * _speed + 1);
        
        Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);
        ricochet = localForward.normalized + hit.normal;

        // Debug.DrawRay(transform.position, transform.rotation.eulerAngles + new Vector3(90, 0, 0), Color.green, _lifetime);

        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, ricochet, _rotationSpeed, 0));
    }

    void Ricochet180Degrees()
    {
        transform.rotation = Quaternion.LookRotation(-transform.forward);

       // normal *= -1;
       _rb.velocity = transform.forward * normal * _speed;
    }
}