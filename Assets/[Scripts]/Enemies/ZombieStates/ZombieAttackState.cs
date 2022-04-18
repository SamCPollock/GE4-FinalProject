using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieStates
{
    GameObject followTarget;
    float attackRange = 2;

    // interface for damageable objet here 
    private IDamageable damageableObject; 

    int movementZHash = Animator.StringToHash("MovementZ");
    int isAttackingHash = Animator.StringToHash("isAttacking");

    public ZombieAttackState(GameObject _followTarget, ZombieComponent zombie, StateMachine zombieStateMachine) : base(zombie, zombieStateMachine)
    {
        followTarget = _followTarget;
        UpdateInterval = 2;

        damageableObject = followTarget.GetComponent<IDamageable>();
    }

    public override void Start()
    {
        //base.Start();
        ownerZombie.zombieNavMeshAgent.isStopped = true;
        ownerZombie.zombieNavMeshAgent.ResetPath();

        ownerZombie.zombieAnimator.SetFloat(movementZHash, 0);
        ownerZombie.zombieAnimator.SetBool(isAttackingHash, true);

    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        // deal damage every intervla (ADD LATER) 
        damageableObject.TakeDamage(ownerZombie.zombieDamage);
    }

    public override void Update()
    {
        //base.Update();
        ownerZombie.transform.LookAt(followTarget.transform.position, Vector3.up);

        float distanceBetween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);
        if (distanceBetween > attackRange)
        {
            stateMachine.ChangeState(ZombieStateType.Follow);
        }
    }

    public override void Exit()
    {
        base.Exit();

        ownerZombie.zombieNavMeshAgent.isStopped = false;
        ownerZombie.zombieAnimator.SetBool(isAttackingHash, false);
    }
}
