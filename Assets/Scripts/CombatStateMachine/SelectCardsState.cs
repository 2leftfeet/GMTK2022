using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectCardsState : BaseCombatState
{
    public SelectCardsState(CombatStateMachine stateMachine) : base("SelectCards", stateMachine) {}

    public List<CardItem> playerSelected;
    public List<CardItem> enemySelected;

    public override void Enter()
    {
        foreach(CardSlot slot in stateMachine.combatSelectSlots)
        {
            slot.isEmpty = true;
        }

        //setup button listener
        stateMachine.cardSelectionConfirmButton.onClick.AddListener(CardsConfirmed);

        //select the cards enemy is about to use (dont ask questions it totally does it)
        var randomCards = stateMachine.enemyAgent.inventory.OrderBy(x => Random.value).Take(stateMachine.enemyAgent.cardPlayedPerTurn).ToList();
        stateMachine.enemyActiveCards = randomCards;

        foreach(var card in randomCards)
        {
            Debug.Log("chosen " + card.item.name);
        }
        //also highlight the cards here
    }

    void CardsConfirmed()
    {
        int cardCount = 0;
        foreach(var slot in stateMachine.combatSelectSlots)
        {
            if(slot.currentCard != null)
            {
                cardCount++;
            } 
        }

        //if no cards chosen, return
        if(cardCount == 0) return;

        foreach(var slot in stateMachine.combatSelectSlots)
        {
            if(slot.currentCard == null) continue;

            stateMachine.activeCards.Add(slot.currentCard);

            slot.currentCard.GetComponent<CardPosition>().GoBackToOldSlot();
        }

        DiceRollSimState diceRollState = new DiceRollSimState(stateMachine, true);
        stateMachine.ChangeState(diceRollState);
    }

    public override void Exit()
    {
        stateMachine.cardSelectionConfirmButton.onClick.RemoveAllListeners();

        foreach(CardSlot slot in stateMachine.combatSelectSlots)
        {
            slot.isEmpty = false;
        }
    }
}
