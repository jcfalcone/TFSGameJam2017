﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerEngagePerception : PerceptionTemplate 
{
    Vector3 VectorUp = Vector3.up;

    public override EnemyStateTemplate.StatesAI Run()
    {
        Transform player = this.parent.GetPlayer();

        Vector3 targetDir = (player.position + this.VectorUp) - this.parent.transform.position;

        float playerAngle = Vector3.Angle(targetDir, this.parent.transform.forward);
        float playerDistance = Vector3.Distance(this.parent.transform.position + this.VectorUp, player.position);

        if (playerDistance > this.parent.GetMinRange())
        {
            return EnemyStateTemplate.StatesAI.Idle;
        }

        if (playerAngle < -this.parent.GetMinAngle() && playerAngle > this.parent.GetMaxAngle())
        {
            return EnemyStateTemplate.StatesAI.Idle;
        }

        RaycastHit hit;

        if (Physics.Linecast(this.parent.transform.position, player.transform.position + this.VectorUp, out hit, this.parent.GetHitLayer()))
        {
            if (!hit.transform.CompareTag("Player"))
            {
                return EnemyStateTemplate.StatesAI.Idle;
            }
        }

        return EnemyStateTemplate.StatesAI.Invalid;
    }
}
