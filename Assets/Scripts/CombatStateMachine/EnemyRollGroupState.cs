using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveCardsState : BaseCombatState
{
    public EnemyMoveCardsState(CombatStateMachine stateMachine) : base("EnemyMoveCards", stateMachine) {}

    static float animationLength = 2f;
    float timer = 0f;

    public override void Enter()
    {
        for(int i = 0; i < stateMachine.enemyActiveCards.Count; i++)
        {
            CardItem card = stateMachine.enemyActiveCards[i];
            card.GetComponent<CardPosition>().currentSlot = stateMachine.combatSelectSlots[i];
        }
    }

    public override void UpdateLogic()
    {
        timer += Time.deltaTime;
        if(timer > animationLength)
        {
            DiceRollSimState diceRollSimState = new DiceRollSimState(stateMachine, false);
            stateMachine.ChangeState(diceRollSimState);
        }
    }

    public override void Exit()
    {
        for(int i = 0; i < stateMachine.enemyActiveCards.Count; i++)
        {
            CardItem card = stateMachine.enemyActiveCards[i];
            card.GetComponent<CardPosition>().GoBackToOldSlot();
        }
    }
}
