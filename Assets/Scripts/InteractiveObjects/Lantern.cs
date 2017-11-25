using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] List<Transform> rope_IK;
    [SerializeField] List<ParticleSystem> fire_IK;
    [SerializeField] Transform lantern;


    int upIndex = 0;
    int downIndex = 0;

    bool bIsBurning = false;
    float currentTime = 0.0f;
    float burnTime = 0.5f;

    void Update()
    {
        if(bIsBurning)
        {
            currentTime += Time.deltaTime;

            if(currentTime >= burnTime)
            {
                currentTime = 0.0f;
                LightFire(upIndex, downIndex);
                upIndex--;
                downIndex++;
            }
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.CompareTag("Projectile"))
        {
            GetComponent<BoxCollider>().enabled = false;

            CheckNodeInRope(c.gameObject);

            Destroy(c.gameObject, 0.5f);
        }
    }

    void CheckNodeInRope(GameObject c)
    {
        float distance = 100.0f;
        int index = -1;

        for (int i = 0; i < rope_IK.Count; i++)
        {
            float checkDistance = Vector3.Distance(rope_IK[i].position, c.transform.position);
            if (checkDistance <= distance)
            {
                distance = checkDistance;
                index = i;
            }
        }

        upIndex = downIndex = index;

        bIsBurning = true;
    }

    void LightFire(int up, int down)
    {
        if (down < rope_IK.Count - 1)
            fire_IK[down].Play();
        else
            DetachLanter();

        if (up > 0)
            fire_IK[up].Play();
    }

    void DetachLanter()
    {
        lantern.parent = null;
        lantern.GetComponent<Rigidbody>().useGravity = true;

        Destroy(gameObject, 1.0f);
    }
}
