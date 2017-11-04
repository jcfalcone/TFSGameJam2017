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
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _toleranceDistanceForIdle;
    [SerializeField]
    private float _toleranceTimeforIdle;

    private Vector3 _previousPosition;
    private float _timeInPosition;

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
        _previousPosition = transform.position;
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);

        if (Vector3.Distance(_previousPosition, transform.position) < _toleranceDistanceForIdle)
        {
            _timeInPosition += Time.deltaTime;
        }
        else
        {
            _timeInPosition = 0;
        }

        if (_timeInPosition >= _toleranceTimeforIdle)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().GetState() == PlayerStateTemplate.States.Solid)
        {
            Ricochet360Degrees();
            Fire();
        }

        if (other.gameObject.tag == "Enemy" || (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().GetState() != PlayerStateTemplate.States.Solid))
        {
            other.gameObject.GetComponent<BaseCharacter>().TakeDamage(_damage);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("ShotReflection"))
        {
            if (other.transform.rotation.x != 0)
            {
                Ricochet90Degrees();
                Fire();
            }
            else
            {
                Destroy(gameObject);
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

        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, ricochet, _rotationSpeed, 0)); transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, ricochet, _rotationSpeed, 0));
    }

    void Ricochet360Degrees()
    {
        Vector3 ricochet = Vector3.zero;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Time.deltaTime * _speed + 1);

        Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);
        ricochet = localForward.normalized + hit.normal;

        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, ricochet, _rotationSpeed, 0)); transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, ricochet, _rotationSpeed, 0));
        transform.eulerAngles = new Vector3(transform.rotation.x + 180, transform.rotation.y, transform.rotation.z);
    }
}