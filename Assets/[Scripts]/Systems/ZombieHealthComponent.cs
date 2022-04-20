using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthComponent : HealthComponent
{
    StateMachine zombieStateMachine;

    private void Awake()
    {
        zombieStateMachine = GetComponent<StateMachine>();
    }

    public override void Destroy()
    {
        Invoke("DestroySelf", 3);
        zombieStateMachine.ChangeState(ZombieStateType.Dead);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
