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
        //sort cards by order of execution
        stateMachine.activeCards.Sort((card1, card2) => card1.item.executionOrder.CompareTo(card2.item.executionOrder));

        //for each card resolve its effect
        foreach(CardItem card in stateMachine.activeCards)
        {
            if(card.owner == stateMachine.playerAgent)
            {
                //player to enemy
                card.item.ResolveItem(ref playerEffects, card.childDice);
            }
            else if(card.owner == stateMachine.enemyAgent)
            {
                //enmy to player
                card.item.ResolveItem(ref enemyEffects, card.childDice);
            }
            else
            {
                Debug.LogError($"Owner of card {card.name} not referenced by combat state machine");
            }
        }

        //apply effects
        stateMachine.playerAgent.DealDamage(enemyEffects.totalDamage * enemyEffects.totalDamageMultiplier);
        stateMachine.playerAgent.Heal(playerEffects.healthToHeal);
        
        stateMachine.playerAgent.AddShield(playerEffects.totalShield * playerEffects.totalShieldMultiplier);


        stateMachine.enemyAgent.DealDamage(playerEffects.totalDamage * playerEffects.totalDamageMultiplier);
        stateMachine.enemyAgent.Heal(enemyEffects.healthToHeal);
        
        stateMachine.enemyAgent.AddShield(enemyEffects.totalShield * enemyEffects.totalShieldMultiplier);
    }
    
}
