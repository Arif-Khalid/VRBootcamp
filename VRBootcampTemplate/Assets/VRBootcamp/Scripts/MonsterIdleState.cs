using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : MonsterState
{
    public override void OnEnterState() {
        monsterStateMachine.animator.SetBool("isWalking", false);
        monsterStateMachine.navMeshAgent.isStopped = true;
    }

    public override void OnExitState() {
    }

    public override void Update() {
    }
}
