using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageState : EnemyStateTemplate
{
    float timeTotal;
    float timeCoolDown;

    Transform player;
    Transform bulletSpawn;
    GameObject bulletPrefab;

    Vector3 vectorUp = Vector3.up;

    AudioSource audio;

    public EngageState()
    {
        this.state = StatesAI.Engage;
        this.perceptions.Add(new PlayerEngagePerception());
    }

    public override void Start()
    {
        base.Start();

        this.timeTotal = 0;
        this.timeCoolDown = 0;

        this.bulletSpawn = this.parent.GetBulletSpawn();
        this.bulletPrefab = this.parent.GetBulletPrefab();
        this.player = this.parent.GetPlayer();
        this.audio = this.parent.GetAudioSource();

        this.modelAnimator.SetBool("Attack", true);
        this.modelAnimator.SetFloat("Speed", 0f);
    }

    public override void End()
    {
        base.End();


        this.modelAnimator.SetBool("Attack", false);
    }

    override public void Tick()
    {
        this.timeTotal += Time.deltaTime;

        if (this.timeTotal > this.timeInState && this.timeCoolDown == 0)
        {
            GameObject.Instantiate(this.bulletPrefab, this.bulletSpawn.transform.position, this.bulletSpawn.transform.rotation);
            this.timeTotal = 0;
            audio.clip = SoundManager.instance.GetClip(SoundManager.AudioClips.Fireball);
            audio.Play();

            this.modelAnimator.SetBool("Attack", false);
            this.timeCoolDown = 0.01f;
        }

        if (this.timeCoolDown != 0)
        {
            this.timeCoolDown += Time.deltaTime;
        }


        if (this.timeCoolDown > 4f)
        {
            this.timeCoolDown = 0;

            this.modelAnimator.SetBool("Attack", true);
        }

        Vector3 targetDir = this.player.position - this.parent.transform.position;
        targetDir.y = 0;
        Vector3 newDir = Vector3.RotateTowards(this.parent.transform.forward, targetDir, this.rotationSpeed * Time.deltaTime, 0.0F);
        this.parent.transform.rotation = Quaternion.LookRotation(newDir);

        this.bulletSpawn.LookAt(this.player.transform.position + this.vectorUp);
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
