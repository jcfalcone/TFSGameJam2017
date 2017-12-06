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
        this.audio = this.parent.GetAudioSource();
        audio.clip = SoundManager.instance.GetClip(SoundManager.AudioClips.GrimLaugh);
        audio.Play();

        this.parent.deathParticle.Play();
        this.modelAnimator.SetBool("Death", true);
    }

    public override void End()
    {
        base.End();

        this.parent.TailParticle.Play();
        this.parent.deathParticle.Stop();
        this.modelAnimator.SetBool("Death", false);

    }

    override public void Tick()
    {
        if (!this.parent.deathIsOver)
        {
            return;
        }

        this.timeTotal += Time.deltaTime;
    }

    override public void FixedTick()
    {
    }

    public override StatesAI NextState()
    {
        if (this.timeTotal > this.timeInState)
        {
            this.parent.transform.position = this.spawnList[this.currSpawnpoint].transform.position;
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
