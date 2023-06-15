using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterStateMachine : MonoBehaviour
{
    public Transform target;
    public float distanceToAttack;
    public Animator animator;
    public float timeBetweenAttacks;
    public float timeBetweenDestinationUpdate;
    public float rotateSpeed;
    public BoxCollider attackCollider;
    public MonsterSoundManager monsterSoundManager;
    private MonsterState currentState;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    public MonsterState monsterChaseState = new MonsterChaseState();
    public MonsterState monsterAttackState = new MonsterAttackState();
    public MonsterState monsterIdleState = new MonsterIdleState();

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        monsterChaseState.monsterStateMachine = this;
        monsterAttackState.monsterStateMachine = this;
        monsterIdleState.monsterStateMachine = this;
        currentState = monsterIdleState;
        monsterSoundManager.StartSounds();
    }
    private void Update() {
        currentState.Update();
    }

    public void changeState(MonsterState nextState) {
        currentState.OnExitState();
        nextState.OnEnterState();
        currentState = nextState;
    }

    public void startChase() {
        changeState(monsterChaseState);
    }

    public void reset() {
        changeState(monsterIdleState);
    }

    public void startAttacking() {
        attackCollider.enabled = true;
    }

    public void stopAttacking() {
        attackCollider.enabled = false;
    }
}
