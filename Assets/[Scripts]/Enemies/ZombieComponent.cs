using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieComponent : MonoBehaviour
{
    // References
    public float zombieDamage = 5;
    public NavMeshAgent zombieNavMeshAgent;
    public Animator zombieAnimator;
    public StateMachine stateMachine;
    public GameObject followTarget;

    private void Awake()
    {
        zombieNavMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();


    }

    void Start()
    {
        Initialize(followTarget);

    }

    void Update()
    {
        
    }

    public void Initialize(GameObject _followTarget)
    {
        followTarget = _followTarget;

        ZombieIdleState idleState = new ZombieIdleState(this, stateMachine);
        stateMachine.AddState(ZombieStateType.Idle, idleState);



        ZombieFollowState followState = new ZombieFollowState(followTarget, this, stateMachine);
        stateMachine.AddState(ZombieStateType.Follow, followState);

        ZombieAttackState attackState = new ZombieAttackState(followTarget, this, stateMachine);
        stateMachine.AddState(ZombieStateType.Attack, attackState);

        ZombieDeadState deadState = new ZombieDeadState(this, stateMachine);
        stateMachine.AddState(ZombieStateType.Dead, deadState);

        stateMachine.Initialize(ZombieStateType.Follow);
    }
}
