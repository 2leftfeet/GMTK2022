using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseCombatState
{
    public DeathState(CombatStateMachine stateMachine) : base("DeathState", stateMachine){}

    public override void Enter()
    {
        stateMachine.menuTable.isCalled = true;

        stateMachine.menuTable.retryButton.onClick.AddListener(DoRetry);
        stateMachine.menuTable.restartButton.onClick.AddListener(DoRestart);
    }

    void DoRetry()
    {
        stateMachine.playerAgent.health = stateMachine.playerAgent.maxHealth;
        stateMachine.playerAgent.shield = 0;

        stateMachine.playerAgent.rerolls = 3;

        stateMachine.enemyAgent.health = stateMachine.enemyAgent.maxHealth;
        stateMachine.enemyAgent.shield = 0;

        foreach(CardItem card in stateMachine.playerAgent.inventory)
        {
            card.RemoveCooldown();
        }

        stateMachine.UpdatePlayerUI();
        stateMachine.UpdateRerollUI();
        stateMachine.UpdateEnemyUI();

        SelectCardsState selectCards = new SelectCardsState(stateMachine);
        stateMachine.ChangeState(selectCards);
    }

    void DoRestart()
    {
        stateMachine.currentRound = 1;

        stateMachine.CleanupEnemy();
        stateMachine.CleanupPlayer();

        InitSetupState initSetup = new InitSetupState(stateMachine);
        stateMachine.ChangeState(initSetup);
    }

    public override void Exit()
    {
        stateMachine.menuTable.retryButton.onClick.RemoveAllListeners();
        stateMachine.menuTable.restartButton.onClick.RemoveAllListeners();

        stateMachine.menuTable.isCalled = false;
    }
}
