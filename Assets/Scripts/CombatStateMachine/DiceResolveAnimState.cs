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
                        dice.MoveToResolution(stateMachine.playerShield.position);
                    }
                    else if(card.owner == stateMachine.enemyAgent)
                    {
                        dice.MoveToResolution(stateMachine.enemyShield.position);
                    }
                }
                else
                {
                    if(card.owner == stateMachine.playerAgent)
                    {
                        dice.MoveToResolution(stateMachine.enemyHealthVial.position);
                    }
                    else if(card.owner == stateMachine.enemyAgent)
                    {
                        dice.MoveToResolution(stateMachine.playerHealthVial.position);
                    }
                }
            }
        }
    }

    bool scalingDown = false;

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
        if(timer > animDuration)
        {
             //go back to select cards, TODO: Death/Reward states
            SelectCardsState selectCards = new SelectCardsState(stateMachine);
            stateMachine.ChangeState(selectCards);
        }
    }

    public override void Exit()
    {
        stateMachine.ClearDice();
        stateMachine.ClearActiveCards();

    }
}
