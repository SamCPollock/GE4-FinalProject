using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFollowState : ZombieStates
{
    GameObject followTarget;
    const float stoppingDistance = 1f;
    int movementZHash = Animator.StringToHash("MovementZ");


    public ZombieFollowState(GameObject _followTarget, ZombieComponent zombie, StateMachine stateMachine) : base(zombie, stateMachine)
    {
        followTarget = _followTarget;
        UpdateInterval = 2;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ownerZombie.zombieNavMeshAgent.SetDestination(followTarget.transform.position);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        ownerZombie.zombieNavMeshAgent.SetDestination(followTarget.transform.position);


        //***ADDED IN THE CODE FROM UPDATE() BECAUSE UPDATE WASN'T CALLING.
        //float moveZ = ownerZombie.zombieNavMeshAgent.velocity.normalized.z != 0f ? 1 : 0f;
        //ownerZombie.zombieAnimator.SetFloat(movementZHash, moveZ);

        //float distanceBetween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);

        //Debug.Log("Distance between: " + distanceBetween);

        //if (distanceBetween < stoppingDistance)
        //{
        //    stateMachine.ChangeState(ZombieStateType.Attack);
        //}

    }

    public override void Update()   //TODO: Figure out why this isn't calling each frame
    {
        base.Update();
        float moveZ = ownerZombie.zombieNavMeshAgent.velocity.normalized.z != 0f ? 1 : 0f;
        ownerZombie.zombieAnimator.SetFloat(movementZHash, moveZ);

        float distanceBetween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);
        if (distanceBetween < stoppingDistance)
        {
            stateMachine.ChangeState(ZombieStateType.Attack);
        }

    }
}
