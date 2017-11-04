using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyStateTemplate 
{
    int currWaypoint = 0;

    [SerializeField]
    private Waypoint[] path;

    public PatrolState()
    {
        this.state = StatesAI.Patrol;
        this.perceptions.Add(new PlayerViewPerception());
    }

    public override void Start()
    {
        this.path = this.parent.GetPath();
    }

    override public void Tick()
    {
        this.parent.transform.position = Vector3.Lerp(this.parent.transform.position, this.path[this.currWaypoint].transform.position, this.speed * Time.deltaTime);

        //Rotation
        Vector3 targetDir = this.path[this.currWaypoint].transform.position - this.parent.transform.position;
        targetDir.y = 0;
        Vector3 newDir = Vector3.RotateTowards(this.parent.transform.forward, targetDir, this.rotationSpeed * Time.deltaTime, 0.0F);

        this.parent.transform.rotation = Quaternion.LookRotation(newDir);
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

        if (Vector3.Distance(this.parent.transform.position, this.path[this.currWaypoint].transform.position) < this.pathProximityTolerance)
        {
            this.SetCurrWaypoint(this.currWaypoint + 1);
            return StatesAI.Idle;
        }

        return base.NextState();
    }

    void SetCurrWaypoint(int _currWaypoint)
    {
        this.currWaypoint = _currWaypoint;

        if (this.currWaypoint >= this.path.Length)
        {
            this.currWaypoint = 0;
        }
    }
}
