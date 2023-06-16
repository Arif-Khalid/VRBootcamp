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
            Vector3 monsterStateMachineXZVector3 = new Vector3(monsterStateMachine.transform.position.x, 0, monsterStateMachine.transform.position.z);
            Vector3 targetXZVector3 = new Vector3(monsterStateMachine.target.transform.position.x, 0, monsterStateMachine.target.transform.position.z);
            float distance = Vector3.Distance(monsterStateMachineXZVector3, targetXZVector3);
            if (distance <= monsterStateMachine.distanceToAttack) {
                monsterStateMachine.changeState(monsterStateMachine.monsterAttackState);
            }
            else {
                if (distance > monsterStateMachine.distanceForSpeedUp) {
                    monsterStateMachine.navMeshAgent.speed = 5;
                }
                else {
                    monsterStateMachine.navMeshAgent.speed = 3;
                }

                monsterStateMachine.navMeshAgent.SetDestination(monsterStateMachine.target.position);
            }
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
    }
}
