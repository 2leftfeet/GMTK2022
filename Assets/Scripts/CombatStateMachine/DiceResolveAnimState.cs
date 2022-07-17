using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceResolveAnimState : BaseCombatState
{
    public DiceResolveAnimState(CombatStateMachine stateMachine) : base("DiceResolveAnim", stateMachine) {}

    static float animDuration = 1.25f;
    float timer = 0.0f;
    List<CardItem> allCards;

    public override void Enter()
    {
        allCards = stateMachine.activeCards.Concat(stateMachine.enemyActiveCards).ToList();

        foreach(CardItem card in allCards)
        {
            foreach(DiceGameObject dice in card.childDice)
            {
                SideType diceType = card.item.diceSides[dice.GetSideUp()].type;

                if(diceType == SideType.Shield || diceType == SideType.ShieldMultiplier)
                {
                    //player shield
                    if(card.owner == stateMachine.playerAgent)
                    {
                        dice.MoveToResolution(stateMachine.playerShield.transform.position);
                    }
                    else if(card.owner == stateMachine.enemyAgent)
                    {
                        dice.MoveToResolution(stateMachine.enemyShield.transform.position);
                    }
                }
                else if(diceType ==SideType.Healing)
                {
                    if(card.owner == stateMachine.playerAgent)
                    {
                        dice.MoveToResolution(stateMachine.playerHealthVial.transform.position);
                    }
                    else if(card.owner == stateMachine.enemyAgent)
                    {
                        dice.MoveToResolution(stateMachine.enemyHealthVial.transform.position);
                    }
                }
                else
                {
                    if(card.owner == stateMachine.playerAgent)
                    {
                        dice.MoveToResolution(stateMachine.enemyHealthVial.transform.position);
                    }
                    else if(card.owner == stateMachine.enemyAgent)
                    {
                        dice.MoveToResolution(stateMachine.playerHealthVial.transform.position);
                    }
                }
            }
        }
    }

    bool scalingDown = false;
    bool updateUI = false;

    public override void UpdateLogic()
    {
        timer += Time.deltaTime;
        if(!scalingDown && timer > 1.0f)
        {
            scalingDown = true;
            foreach(CardItem card in allCards)
            {
                foreach(DiceGameObject dice in card.childDice)
                {
                    dice.ScaleDown();
                }
            }
        }


        if(!updateUI && timer > animDuration)
        {
            updateUI = true;
            stateMachine.UpdatePlayerUI();
            stateMachine.UpdateEnemyUI();
        }
        
        if(timer > animDuration + 1f)
        {
             //go back to select cards, TODO: Death/Reward states
            if(stateMachine.playerAgent.health <= 0)
            {
                DeathState deathState = new DeathState(stateMachine);
                stateMachine.ChangeState(deathState);
            }
            else if(stateMachine.enemyAgent.health <= 0)
            {
                RewardsPhaseState rewardsState = new RewardsPhaseState(stateMachine);
                stateMachine.ChangeState(rewardsState);
            }
            else
            {
                SelectCardsState selectCards = new SelectCardsState(stateMachine);
                stateMachine.ChangeState(selectCards);
            }
        }
    }

    public override void Exit()
    {
        stateMachine.UpdatePlayerUI();
        stateMachine.UpdateEnemyUI();

        stateMachine.ClearDice();
        stateMachine.ClearActiveCards();

    }
}
