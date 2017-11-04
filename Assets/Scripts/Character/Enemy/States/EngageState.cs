﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageState : EnemyStateTemplate 
{
    float timeTotal;

    Transform player;
    Transform bulletSpawn;
    GameObject bulletPrefab;

    public EngageState()
    {
        this.state = StatesAI.Engage;
        this.perceptions.Add(new PlayerEngagePerception());
    }

    public override void Start()
    {
        base.Start();

        this.timeTotal = 0;

        this.bulletSpawn = this.parent.GetBulletSpawn();
        this.bulletPrefab = this.parent.GetBulletPrefab();
        this.player = this.parent.GetPlayer();
    }

    override public void Tick()
    {
        this.timeTotal += Time.deltaTime;

        if (this.timeTotal > this.timeInState)
        {
            GameObject.Instantiate(this.bulletPrefab, this.bulletSpawn.transform.position, this.bulletSpawn.transform.rotation);
            this.timeTotal = 0;
        }

        Vector3 targetDir = this.player.position - this.parent.transform.position;
        targetDir.y = 0;
        Vector3 newDir = Vector3.RotateTowards(this.parent.transform.forward, targetDir, this.rotationSpeed * Time.deltaTime, 0.0F);
        this.parent.transform.rotation = Quaternion.LookRotation(newDir);

        this.bulletSpawn.LookAt(this.player);
    }

    override public void FixedTick()
    {
    }

    public override StatesAI NextState()
    {
        if (this.parent.GetHealth() <= 0)
        {
            return StatesAI.Die;
        }

        StatesAI perceptionState = this.Perceptions();

        if (perceptionState != StatesAI.Invalid)
        {
            return perceptionState;
        }

        return base.NextState();
    }
}
