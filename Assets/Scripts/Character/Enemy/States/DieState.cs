using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : EnemyStateTemplate
{
    float timeTotal;

    int currSpawnpoint = 0;

    Spawnpoint[] spawnList;

    AudioSource audio;

    public DieState()
    {
        this.state = StatesAI.Die;
    }

    public override void Start()
    {
        base.Start();

        this.timeTotal = 0;

        this.spawnList = this.parent.GetSpawnpoint();

        this.parent.transform.position = this.spawnList[this.currSpawnpoint].transform.position;
        this.audio = this.parent.GetAudioSource();
        audio.clip = SoundManager.instance.GetClip(SoundManager.AudioClips.GrimLaugh);
        audio.Play();
    }

    override public void Tick()
    {
        this.timeTotal += Time.deltaTime;
    }

    override public void FixedTick()
    {
    }

    public override StatesAI NextState()
    {
        if (this.timeTotal > this.timeInState)
        {
            this.parent.Init();
            this.SetCurrSpawnpoint(this.currSpawnpoint + 1);
            return StatesAI.Idle;
        }

        return base.NextState();
    }

    void SetCurrSpawnpoint(int _currSpawnpoint)
    {
        this.currSpawnpoint = _currSpawnpoint;

        if (this.currSpawnpoint >= this.spawnList.Length)
        {
            this.currSpawnpoint = 0;
        }
    }
}
