using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStates : State
{
    protected ZombieComponent ownerZombie; 
    protected ZombieStates(ZombieComponent zombie, StateMachine stateMachine) : base(stateMachine)
    {
        ownerZombie = zombie; 
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public enum ZombieStateType
{
    Idle, 
    Attack, 
    Follow, 
    Dead
}
