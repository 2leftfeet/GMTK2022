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
        stateMachine.playerAgent.health = stateMachine.playerAgent.maxHealth;
        stateMachine.playerAgent.shield = 0;

        stateMachine.UpdatePlayerUI();
        stateMachine.UpdateRerollUI();

        stateMachine.currentRound ++;

        SpawnEnemyState spawnEnemyState = new SpawnEnemyState(stateMachine);
        stateMachine.ChangeState(spawnEnemyState);
    }
}
