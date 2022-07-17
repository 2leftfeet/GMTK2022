using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResolveEffectsState : BaseCombatState
{
    public ResolveEffectsState(CombatStateMachine stateMachine) : base("ResolveEffects", stateMachine)
    {}


    public override void Enter()
    {
        RoundEffects playerEffects = new RoundEffects();
        RoundEffects enemyEffects = new RoundEffects();

        List<CardItem> allCards = stateMachine.activeCards.Concat(stateMachine.enemyActiveCards).ToList();
        //sort cards by order of execution
        allCards.Sort((card1, card2) => card1.item.executionOrder.CompareTo(card2.item.executionOrder));

        //for each card resolve its effect
        foreach(CardItem card in allCards)
        {
            if(card.owner == stateMachine.playerAgent)
            {
                //player to enemy
                card.item.ResolveItem(ref playerEffects, ref enemyEffects, card.childDice, stateMachine.playerAgent, stateMachine.enemyAgent);
            }
            else if(card.owner == stateMachine.enemyAgent)
            {
                //enmy to player
                card.item.ResolveItem(ref enemyEffects, ref playerEffects, card.childDice, stateMachine.enemyAgent, stateMachine.playerAgent);
            }
            else
            {
                Debug.LogError($"Owner of card {card.name} not referenced by combat state machine");
            }
        }

        //apply effects
        stateMachine.playerAgent.AddShield(playerEffects.totalShield * playerEffects.totalShieldMultiplier);
        
        stateMachine.playerAgent.DealDamage(enemyEffects.totalDamage * enemyEffects.totalDamageMultiplier + enemyEffects.unscaledDamage);
        stateMachine.playerAgent.Heal(playerEffects.healthToHeal);
        

        stateMachine.enemyAgent.AddShield(enemyEffects.totalShield * enemyEffects.totalShieldMultiplier);

        stateMachine.enemyAgent.DealDamage(playerEffects.totalDamage * playerEffects.totalDamageMultiplier + enemyEffects.unscaledDamage);
        stateMachine.enemyAgent.Heal(enemyEffects.healthToHeal);
        
        DiceResolveAnimState diceAnim = new DiceResolveAnimState(stateMachine);
        stateMachine.ChangeState(diceAnim);
    }
}
