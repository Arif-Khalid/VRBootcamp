using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterState
{
    private float currentTime;
    public override void OnEnterState() {
        currentTime = 0;
    }

    public override void OnExitState() {
    }

    public override void Update() {
        if (currentTime >= monsterStateMachine.timeBetweenAttacks) {
            if (Vector3.Distance(monsterStateMachine.transform.position, monsterStateMachine.target.position) > monsterStateMachine.distanceToAttack) {
                // Change State
                monsterStateMachine.changeState(monsterStateMachine.monsterChaseState);
            }
            else {
                // Attack
                monsterStateMachine.animator.SetTrigger("attack");
            }
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
    }
}
