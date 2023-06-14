using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseState : MonsterState
{
    private float currentTime;
    public override void OnEnterState() {
        monsterStateMachine.monsterSoundManager.StartSounds();
        monsterStateMachine.animator.SetBool("isWalking", true);
        monsterStateMachine.navMeshAgent.isStopped = false;
        currentTime = 0;
    }

    public override void OnExitState() {
        monsterStateMachine.animator.SetBool("isWalking", false);
    }

    public override void Update() {
        if (currentTime >= monsterStateMachine.timeBetweenDestinationUpdate) {
            if (Vector3.Distance(monsterStateMachine.transform.position, monsterStateMachine.target.position) <= monsterStateMachine.distanceToAttack) {
                monsterStateMachine.changeState(monsterStateMachine.monsterAttackState);
            }
            else {
                monsterStateMachine.navMeshAgent.SetDestination(monsterStateMachine.target.position);
            }
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
    }
}
