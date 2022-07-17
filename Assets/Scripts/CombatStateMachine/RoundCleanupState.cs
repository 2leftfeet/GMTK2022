using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCleanupState : BaseCombatState
{
    public RoundCleanupState(CombatStateMachine stateMachine) : base("RoundCleanup", stateMachine) {}

    public override void Enter()
    {
        stateMachine.CleanupEnemy();
        stateMachine.CleanupRewards();

        stateMachine.playerAgent.rerolls += 3;
        stateMachine.playerAgent.shield = 0;

        stateMachine.UpdatePlayerUI();

        stateMachine.currentRound ++;

        SpawnEnemyState spawnEnemyState = new SpawnEnemyState(stateMachine);
        stateMachine.ChangeState(spawnEnemyState);
    }
}
