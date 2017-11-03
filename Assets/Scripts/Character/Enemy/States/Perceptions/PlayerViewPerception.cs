using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerViewPerception : PerceptionTemplate 
{

    public override EnemyStateTemplate.StatesAI Run()
    {
        Transform player = this.parent.GetPlayer();

        Vector3 targetDir = player.position - this.parent.transform.position;

        float playerAngle = Vector3.Angle(targetDir, this.parent.transform.forward);
        float playerDistance = Vector3.Distance(this.parent.transform.position, player.position);

        if (playerDistance < this.parent.GetMinRange())
        {

            if (playerAngle > -this.parent.GetMinAngle() && playerAngle < this.parent.GetMaxAngle())
            {

                RaycastHit hit;

                if (Physics.Linecast(this.parent.transform.position, player.transform.position, out hit))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        return EnemyStateTemplate.StatesAI.Engage;
                    }
                }
            }
        }

        return EnemyStateTemplate.StatesAI.Invalid;
    }
}
