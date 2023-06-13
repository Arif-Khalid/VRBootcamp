using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterState
{
    public MonsterStateMachine monsterStateMachine;
    public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract void Update();
}
