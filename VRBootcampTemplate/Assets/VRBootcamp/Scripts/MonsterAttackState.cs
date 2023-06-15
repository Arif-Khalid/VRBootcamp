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
        Vector3 dir = monsterStateMachine.target.position - monsterStateMachine.transform.position;
        monsterStateMachine.transform.forward = Vector3.Lerp(monsterStateMachine.transform.forward, new Vector3(dir.x, 0, dir.z), Time.deltaTime * monsterStateMachine.rotateSpeed);
        if (currentTime >= monsterStateMachine.timeBetweenAttacks) {
            Vector3 monsterStateMachineXZVector3 = new Vector3(monsterStateMachine.transform.position.x, 0, monsterStateMachine.transform.position.z);
            Vector3 targetXZVector3 = new Vector3(monsterStateMachine.target.transform.position.x, 0, monsterStateMachine.target.transform.position.z);
            if (Vector3.Distance(monsterStateMachineXZVector3, targetXZVector3) > monsterStateMachine.distanceToAttack) {
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
