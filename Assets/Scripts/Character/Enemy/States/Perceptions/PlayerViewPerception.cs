using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerViewPerception : PerceptionTemplate 
{
    Vector3 VectorUp = Vector3.up;

    public override EnemyStateTemplate.StatesAI Run()
    {
        Transform player = this.parent.GetPlayer();

        Vector3 targetDir = (player.position + this.VectorUp) - this.parent.transform.position;

        float playerAngle = Vector3.Angle(targetDir, this.parent.transform.forward);
        float playerDistance = Vector3.Distance(this.parent.transform.position + this.VectorUp, player.position);

        Debug.DrawLine(this.parent.transform.position, player.transform.position, Color.red);

        if (playerDistance < this.parent.GetMinRange())
        {

            if (playerAngle > -this.parent.GetMinAngle() && playerAngle < this.parent.GetMaxAngle())
            {
                RaycastHit hit;

                if (Physics.Linecast(this.parent.transform.position, player.transform.position + this.VectorUp, out hit, this.parent.GetHitLayer()))
                {
                    if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("PlayerCollider"))
                    {
                        return EnemyStateTemplate.StatesAI.Engage;
                    }
                }
            }
        }

        return EnemyStateTemplate.StatesAI.Invalid;
    }
}
